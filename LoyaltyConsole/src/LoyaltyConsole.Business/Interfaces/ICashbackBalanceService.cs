using LoyaltyConsole.Business.DTOs.CashbackBalanceDtos;
using LoyaltyConsole.Core.Models;
using System.Linq.Expressions;

namespace LoyaltyConsole.Business.Interfaces
{
    public interface ICashbackBalanceService
    {
        Task<bool> IsExist(Expression<Func<CashbackBalance, bool>> expression);
        Task<CashbackBalanceGetDto> CreateAsync(CashbackBalanceCreateDto dto);
        Task UpdateAsync(int? id, CashbackBalanceUpdateDto dto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task<CashbackBalanceGetDto> GetById(int id);
        Task<ICollection<CashbackBalanceGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<CashbackBalance, bool>>? expression = null, params string[] includes);
        Task<CashbackBalanceGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<CashbackBalance, bool>>? expression = null, params string[] includes);
    }
}
