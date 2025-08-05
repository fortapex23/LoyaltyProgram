using System.Linq.Expressions;
using LoyaltyConsole.Business.DTOs.TransactionDtos;
using LoyaltyConsole.Core.Models;

namespace LoyaltyConsole.Business.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> IsExist(Expression<Func<Transaction, bool>> expression);
        Task<TransactionGetDto> CreateAsync(TransactionCreateDto dto);
        Task UpdateAsync(int? id, TransactionUpdateDto dto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task<TransactionGetDto> GetById(int id);
        Task<ICollection<TransactionGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<Transaction, bool>>? expression = null, params string[] includes);
        Task<TransactionGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<Transaction, bool>>? expression = null, params string[] includes);
    }
}
