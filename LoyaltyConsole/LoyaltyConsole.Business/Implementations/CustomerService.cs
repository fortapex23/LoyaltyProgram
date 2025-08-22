using AutoMapper;
using LoyaltyConsole.Business.DTOs.CustomerDtos;
using LoyaltyConsole.Business.ExternalServices.Interfaces;
using LoyaltyConsole.Business.Interfaces;
using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LoyaltyConsole.Business.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICashbackBalanceRepository _cashbackBalanceRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, ICashbackBalanceRepository cashbackBalanceRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _cashbackBalanceRepository = cashbackBalanceRepository;
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

            await _customerRepository.CreateAsync(customer);
            await _customerRepository.CommitAsync();

            return _mapper.Map<CustomerGetDto>(customer);
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new ArgumentException("Invalid ID");

            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) throw new Exception("Customer not found.");

            _customerRepository.Delete(customer);
            await _customerRepository.CommitAsync();
        }


        public async Task<ICollection<CustomerGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<Customer, bool>>? expression = null, params string[] includes)
        {
            var customers = await _customerRepository.GetByExpression(asnotracking, expression, includes).ToListAsync();

            return _mapper.Map<ICollection<CustomerGetDto>>(customers);
        }

        public async Task<CustomerGetDto> GetById(int id)
        {
            if (id < 1) throw new Exception();

            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) throw new Exception("Customer not found");

            return _mapper.Map<CustomerGetDto>(customer);
        }

        public async Task<CustomerGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<Customer, bool>>? expression = null, params string[] includes)
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

            _mapper.Map(dto, customer);

            customer.UpdatedDate = DateTime.Now;

            await _customerRepository.CommitAsync();
        }
    }
}
