using LoyaltyConsole.Business.DTOs.CustomerDtos;
using LoyaltyConsole.Core.Models;
using System.Linq.Expressions;

namespace LoyaltyConsole.Business.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> IsExist(Expression<Func<Customer, bool>> expression);
        Task<ICollection<CustomerGetDto>> SearchCustomer(string fullName);
        Task<CustomerGetDto> CreateAsync(CustomerCreateDto dto);
        Task UpdateAsync(int? id, CustomerUpdateDto dto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task<CustomerGetDto> GetById(int id);
        Task<ICollection<CustomerGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<Customer, bool>>? expression = null, params string[] includes);
        Task<CustomerGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<Customer, bool>>? expression = null, params string[] includes);
    }
}
