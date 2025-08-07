using LoyaltyConsole.Business.DTOs.CustomerTagDtos;
using LoyaltyConsole.Core.Models;
using System.Linq.Expressions;

namespace LoyaltyConsole.Business.Interfaces
{
    public interface ICustomerTagService
    {
        Task<bool> IsExist(Expression<Func<CustomerTag, bool>> expression);
        Task<CustomerTagGetDto> CreateAsync(CustomerTagCreateDto dto);
        Task UpdateAsync(int? id, CustomerTagUpdateDto dto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task<CustomerTagGetDto> GetById(int id);
        Task<ICollection<CustomerTagGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<CustomerTag, bool>>? expression = null, params string[] includes);
        Task<CustomerTagGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<CustomerTag, bool>>? expression = null, params string[] includes);
    }
}
