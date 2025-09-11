using AutoMapper;
using LoyaltyConsole.Business.DTOs.CustomerDtos;
using LoyaltyConsole.Business.Interfaces;
using LoyaltyConsole.Business.Utilities.Extensions;
using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Hosting;
using LoyaltyConsole.Business.ExternalServices.Interfaces;

namespace LoyaltyConsole.Business.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerImageRepository _customerimageRepository;
        private readonly ICashbackBalanceRepository _cashbackBalanceRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IPhotoService _photoService;

        public CustomerService(
            ICustomerRepository customerRepository,
            IMapper mapper,
            ICashbackBalanceRepository cashbackBalanceRepository,
            ICustomerImageRepository customerimageRepository,
            IWebHostEnvironment env,
            IPhotoService photoService)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _cashbackBalanceRepository = cashbackBalanceRepository;
            _customerimageRepository = customerimageRepository;
            _env = env;
            _photoService = photoService;
        }

        public async Task<ICollection<CustomerGetDto>> GetByExpression(
            bool asnotracking = false,
            Expression<Func<Customer, bool>>? expression = null,
            params string[] includes)
        {
            var customers = await _customerRepository.GetByExpression(asnotracking, expression, includes).ToListAsync();
            return _mapper.Map<ICollection<CustomerGetDto>>(customers);
        }

        public Task<bool> IsExist(Expression<Func<Customer, bool>> expression)
        {
            return _customerRepository.Table.AnyAsync(expression);
        }

        public async Task<CustomerGetDto> CreateAsync(CustomerCreateDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            customer.CreatedDate = DateTime.Now;
            customer.UpdatedDate = DateTime.Now;

            if (customer.Birthday >= DateTime.Now)
                throw new Exception("Invalid Birthday");

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var uploadResult = await _photoService.AddPhotoAsync(dto.ImageFile);

                if (uploadResult.Error != null)
                    throw new Exception(uploadResult.Error.Message);

                CustomerImage cusImage = new CustomerImage
                {
                    ImageUrl = uploadResult.SecureUrl.ToString(),
                    PublicId = uploadResult.PublicId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Customer = customer
                };

                await _customerimageRepository.CreateAsync(cusImage);
            }

            await _customerRepository.CreateAsync(customer);
            await _customerRepository.CommitAsync();

            var cashbackBalance = new CashbackBalance()
            {
                CustomerId = customer.Id,
                CashbackRedeemed = 0,
                TotalCashback = 0,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            await _cashbackBalanceRepository.CreateAsync(cashbackBalance);
            await _cashbackBalanceRepository.CommitAsync();

            return _mapper.Map<CustomerGetDto>(customer);
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new ArgumentException("Invalid ID");

            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) throw new Exception("Customer not found.");

            if (customer.CustomerImage != null)
            {
                if (!string.IsNullOrEmpty(customer.CustomerImage.PublicId))
                {
                    await _photoService.DeletePhotoAsync(customer.CustomerImage.PublicId);
                }

                _customerimageRepository.Delete(customer.CustomerImage);
            }

            _customerRepository.Delete(customer);
            await _customerRepository.CommitAsync();
        }

        public async Task<CustomerGetDto> GetById(int id)
        {
            if (id < 1) throw new Exception();

            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) throw new Exception("Customer not found");

            return _mapper.Map<CustomerGetDto>(customer);
        }

        public async Task<CustomerGetDto> GetSingleByExpression(
            bool asnotracking = false,
            Expression<Func<Customer, bool>>? expression = null,
            params string[] includes)
        {
            var customer = await _customerRepository.GetByExpression(asnotracking, expression, includes).FirstOrDefaultAsync();
            if (customer == null) throw new Exception("Customer not found");

            return _mapper.Map<CustomerGetDto>(customer);
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id < 1) throw new Exception();

            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) throw new Exception("Customer not found.");

            customer.IsDeleted = true;
            await _customerRepository.CommitAsync();
        }

        public async Task UpdateAsync(int? id, CustomerUpdateDto dto)
        {
            if (id < 1 || id is null) throw new NullReferenceException("id is invalid");

            var customer = await _customerRepository.GetByIdAsync((int)id);
            if (customer == null) throw new Exception("Customer not found");

            if (customer.Birthday >= DateTime.Now)
                throw new Exception("Invalid Birthday");

            _mapper.Map(dto, customer);
            customer.UpdatedDate = DateTime.Now;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var uploadResult = await _photoService.AddPhotoAsync(dto.ImageFile);

                if (uploadResult.Error != null)
                    throw new Exception(uploadResult.Error.Message);

                CustomerImage cusImage = new CustomerImage
                {
                    ImageUrl = uploadResult.SecureUrl.ToString(),
                    PublicId = uploadResult.PublicId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Customer = customer
                };

                await _customerimageRepository.CreateAsync(cusImage);
            }

            await _customerRepository.CommitAsync();
        }

    }
}
