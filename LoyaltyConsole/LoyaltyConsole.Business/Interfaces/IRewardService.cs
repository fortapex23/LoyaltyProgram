using LoyaltyConsole.Business.DTOs.RewardDtos;
using LoyaltyConsole.Core.Models;
using System.Linq.Expressions;

namespace LoyaltyConsole.Business.Interfaces
{
    public interface IRewardService
    {
        Task<bool> IsExist(Expression<Func<Reward, bool>> expression);
        Task<RewardGetDto> CreateAsync(RewardCreateDto dto);
        //Task UpdateAsync(int? id, RewardUpdateDto dto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task<RewardGetDto> GetById(int id);
        Task<ICollection<RewardGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<Reward, bool>>? expression = null, params string[] includes);
        Task<RewardGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<Reward, bool>>? expression = null, params string[] includes);
    }
}
