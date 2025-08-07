using AutoMapper;
using LoyaltyConsole.Business.DTOs.AppUserRewardDtos;
using System.Linq.Expressions;
using LoyaltyConsole.Business.Interfaces;
using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyConsole.Business.Implementations
{
    public class AppUserRewardService : IAppUserRewardService
    {
        private readonly IAppUserRewardRepository _appUserRewardRepository;
        private readonly IMapper _mapper;

        public AppUserRewardService(IAppUserRewardRepository appUserRewardRepository, IMapper mapper)
        {
            _appUserRewardRepository = appUserRewardRepository;
            _mapper = mapper;
        }

        public Task<bool> IsExist(Expression<Func<AppUserReward, bool>> expression)
        {
            return _appUserRewardRepository.Table.AnyAsync(expression);
        }

        public async Task<AppUserRewardGetDto> CreateAsync(AppUserRewardCreateDto dto)
        {
            var appUserReward = _mapper.Map<AppUserReward>(dto);
            appUserReward.CreatedDate = DateTime.Now;
            appUserReward.UpdatedDate = DateTime.Now;

            await _appUserRewardRepository.CreateAsync(appUserReward);
            await _appUserRewardRepository.CommitAsync();

            return _mapper.Map<AppUserRewardGetDto>(appUserReward);
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new ArgumentException("Invalid ID");

            var appUserReward = await _appUserRewardRepository.GetByIdAsync(id);
            if (appUserReward == null) throw new Exception("AppUserReward not found.");

            _appUserRewardRepository.Delete(appUserReward);
            await _appUserRewardRepository.CommitAsync();
        }

        public async Task<ICollection<AppUserRewardGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<AppUserReward, bool>>? expression = null, params string[] includes)
        {
            var appUserRewards = await _appUserRewardRepository.GetByExpression(asnotracking, expression, includes).ToListAsync();

            return _mapper.Map<ICollection<AppUserRewardGetDto>>(appUserRewards);
        }

        public async Task<AppUserRewardGetDto> GetById(int id)
        {
            if (id < 1) throw new Exception();

            var appUserReward = await _appUserRewardRepository.GetByIdAsync(id);
            if (appUserReward == null) throw new Exception("AppUserReward not found");

            return _mapper.Map<AppUserRewardGetDto>(appUserReward);
        }

        public async Task<AppUserRewardGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<AppUserReward, bool>>? expression = null, params string[] includes)
        {
            var appUserReward = await _appUserRewardRepository.GetByExpression(asnotracking, expression, includes).FirstOrDefaultAsync();
            if (appUserReward == null) throw new Exception("AppUserReward not found");

            return _mapper.Map<AppUserRewardGetDto>(appUserReward);
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id < 1) throw new Exception();

            var appUserReward = await _appUserRewardRepository.GetByIdAsync(id);
            if (appUserReward == null) throw new Exception("AppUserReward not found.");

            appUserReward.IsDeleted = true;

            await _appUserRewardRepository.CommitAsync();
        }

        public async Task UpdateAsync(int? id, AppUserRewardUpdateDto dto)
        {
            if (id < 1 || id is null) throw new NullReferenceException("id is invalid");

            var appUserReward = await _appUserRewardRepository.GetByIdAsync((int)id);
            if (appUserReward == null) throw new Exception("AppUserReward not found");

            _mapper.Map(dto, appUserReward);

            appUserReward.UpdatedDate = DateTime.Now;

            await _appUserRewardRepository.CommitAsync();
        }
    }
}
