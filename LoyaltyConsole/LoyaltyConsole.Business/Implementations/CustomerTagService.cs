using AutoMapper;
using LoyaltyConsole.Business.DTOs.CustomerTagDtos;
using System.Linq.Expressions;
using LoyaltyConsole.Business.Interfaces;
using LoyaltyConsole.Core.Repositories;
using LoyaltyConsole.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyConsole.Business.Implementations
{
    public class CustomerTagService : ICustomerTagService
    {
        private readonly ICustomerTagRepository _customerTagRepository;
        private readonly IMapper _mapper;

        public CustomerTagService(ICustomerTagRepository customerTagRepository, IMapper mapper)
        {
            _customerTagRepository = customerTagRepository;
            _mapper = mapper;
        }

        public Task<bool> IsExist(Expression<Func<CustomerTag, bool>> expression)
        {
            return _customerTagRepository.Table.AnyAsync(expression);
        }

        public async Task<CustomerTagGetDto> CreateAsync(CustomerTagCreateDto dto)
        {
            var customerTag = _mapper.Map<CustomerTag>(dto);
            customerTag.CreatedDate = DateTime.Now;
            customerTag.UpdatedDate = DateTime.Now;

            await _customerTagRepository.CreateAsync(customerTag);
            await _customerTagRepository.CommitAsync();

            return _mapper.Map<CustomerTagGetDto>(customerTag);
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new ArgumentException("Invalid ID");

            var customerTag = await _customerTagRepository.GetByIdAsync(id);
            if (customerTag == null) throw new Exception("CustomerTag not found.");

            _customerTagRepository.Delete(customerTag);
            await _customerTagRepository.CommitAsync();
        }


        public async Task<ICollection<CustomerTagGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<CustomerTag, bool>>? expression = null, params string[] includes)
        {
            var customerTags = await _customerTagRepository.GetByExpression(asnotracking, expression, includes).ToListAsync();

            return _mapper.Map<ICollection<CustomerTagGetDto>>(customerTags);
        }

        public async Task<CustomerTagGetDto> GetById(int id)
        {
            if (id < 1) throw new Exception();

            var customerTag = await _customerTagRepository.GetByIdAsync(id);
            if (customerTag == null) throw new Exception("CustomerTag not found");

            return _mapper.Map<CustomerTagGetDto>(customerTag);
        }

        public async Task<CustomerTagGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<CustomerTag, bool>>? expression = null, params string[] includes)
        {
            var customerTag = await _customerTagRepository.GetByExpression(asnotracking, expression, includes).FirstOrDefaultAsync();
            if (customerTag == null) throw new Exception("CustomerTag not found");

            return _mapper.Map<CustomerTagGetDto>(customerTag);
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id < 1) throw new Exception();

            var customerTag = await _customerTagRepository.GetByIdAsync(id);
            if (customerTag == null) throw new Exception("CustomerTag not found.");

            customerTag.IsDeleted = true;

            await _customerTagRepository.CommitAsync();
        }

        public async Task UpdateAsync(int? id, CustomerTagUpdateDto dto)
        {
            if (id < 1 || id is null) throw new NullReferenceException("id is invalid");

            var customerTag = await _customerTagRepository.GetByIdAsync((int)id);
            if (customerTag == null) throw new Exception("CustomerTag not found");

            _mapper.Map(dto, customerTag);

            customerTag.UpdatedDate = DateTime.Now;

            await _customerTagRepository.CommitAsync();
        }
    }
}
