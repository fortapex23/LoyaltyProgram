using LoyaltyConsole.Business.DTOs.AppUserRewardDtos;
using LoyaltyConsole.Core.Models;
using System.Linq.Expressions;

namespace LoyaltyConsole.Business.Interfaces
{
    public interface IAppUserRewardService
    {
        Task<bool> IsExist(Expression<Func<AppUserReward, bool>> expression);
        Task<AppUserRewardGetDto> CreateAsync(AppUserRewardCreateDto dto);
        Task UpdateAsync(int? id, AppUserRewardUpdateDto dto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task<AppUserRewardGetDto> GetById(int id);
        Task<ICollection<AppUserRewardGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<AppUserReward, bool>>? expression = null, params string[] includes);
        Task<AppUserRewardGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<AppUserReward, bool>>? expression = null, params string[] includes);
    }
}
