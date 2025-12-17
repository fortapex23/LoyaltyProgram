
using LoyaltyConsole.Business.DTOs.CustomerDtos;
using LoyaltyConsole.Business.PaginatedLists;
using LoyaltyConsole.Core.Models;
using System.Linq.Expressions;

namespace LoyaltyConsole.Business.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> IsExist(Expression<Func<Customer, bool>> expression);
        //Task<ICollection<CustomerListDto>> SearchCustomer(string input);
        //Task<ICollection<CustomerListDto>> GetListAsync();
        Task<PagedResult<CustomerListDto>> GetPagedListAsync(int page, int pageSize, string? search);
        Task<CustomerGetDto> CreateAsync(CustomerCreateDto dto);
        Task UpdateAsync(int? id, CustomerUpdateDto dto);
        Task DeleteAsync(int id);
        Task<ICollection<CustomerGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<Customer, bool>>? expression = null, params string[] includes);
        Task<CustomerGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<Customer, bool>>? expression = null, params string[] includes);
    }
}
