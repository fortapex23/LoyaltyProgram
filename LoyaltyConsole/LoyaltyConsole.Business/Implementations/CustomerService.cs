using AutoMapper;
using LoyaltyConsole.Business.DTOs.CustomerDtos;
using LoyaltyConsole.Business.Interfaces;
using LoyaltyConsole.Business.Utilities.Extensions;
using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Hosting;

namespace LoyaltyConsole.Business.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerImageRepository _customerimageRepository;
        private readonly ICashbackBalanceRepository _cashbackBalanceRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CustomerService(
            ICustomerRepository customerRepository,
            IMapper mapper,
            ICashbackBalanceRepository cashbackBalanceRepository,
            ICustomerImageRepository customerimageRepository,
            IWebHostEnvironment env)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _cashbackBalanceRepository = cashbackBalanceRepository;
            _customerimageRepository = customerimageRepository;
            _env = env;
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

            //if (dto.Image != null)
            //{
            //    string ext = Path.GetExtension(dto.Image.FileName).ToLower();
            //    if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
            //        throw new Exception("Only .jpg, .jpeg, .png files are allowed");

            //    if (dto.Image.Length > 3 * 1024 * 1024)
            //        throw new Exception("Image size must be less than 3 MB");

            //    string fileName = dto.Image.CreateFileAsync(_env.WebRootPath, "uploads/customers");

            //    CustomerImage cusImage = new CustomerImage
            //    {
            //        ImageUrl = fileName,
            //        CreatedDate = DateTime.Now,
            //        UpdatedDate = DateTime.Now,
            //        Customer = customer
            //    };

            //    await _customerimageRepository.CreateAsync(cusImage);
            //}

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
                customer.CustomerImage.ImageUrl.DeleteFile(_env.WebRootPath, "uploads/customers");
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

            if (dto.Image != null)
            {
                string ext = Path.GetExtension(dto.Image.FileName).ToLower();
                if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                    throw new Exception("Only .jpg, .jpeg, .png files are allowed");

                if (dto.Image.Length > 3 * 1024 * 1024)
                    throw new Exception("Image size must be less than 3 MB");

                if (customer.CustomerImage != null)
                {
                    customer.CustomerImage.ImageUrl.DeleteFile(_env.WebRootPath, "uploads/customers");
                }

                string fileName = dto.Image.CreateFileAsync(_env.WebRootPath, "uploads/customers");

                if (customer.CustomerImage == null)
                {
                    CustomerImage cusImage = new CustomerImage
                    {
                        CustomerId = customer.Id,
                        ImageUrl = fileName,
                        Customer = customer,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };

                    await _customerimageRepository.CreateAsync(cusImage);
                }
                else
                {
                    customer.CustomerImage.ImageUrl = fileName;
                    customer.CustomerImage.UpdatedDate = DateTime.Now;
                }
            }

            await _customerRepository.CommitAsync();
        }
    }
}
