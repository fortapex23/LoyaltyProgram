using AutoMapper;
using LoyaltyConsole.Business.DTOs.RewardDtos;
using System.Linq.Expressions;
using LoyaltyConsole.Business.Interfaces;
using LoyaltyConsole.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using LoyaltyConsole.Core.Models;

namespace LoyaltyConsole.Business.Implementations
{
    public class RewardService : IRewardService
    {
        private readonly IRewardRepository _rewardRepository;
        private readonly IMapper _mapper;

        public RewardService(IRewardRepository rewardRepository, IMapper mapper)
        {
            _rewardRepository = rewardRepository;
            _mapper = mapper;
        }

        public Task<bool> IsExist(Expression<Func<Reward, bool>> expression)
        {
            return _rewardRepository.Table.AnyAsync(expression);
        }

        public async Task<RewardGetDto> CreateAsync(RewardCreateDto dto)
        {
            var reward = _mapper.Map<Reward>(dto);
            reward.CreatedDate = DateTime.Now;
            reward.UpdatedDate = DateTime.Now;

            await _rewardRepository.CreateAsync(reward);
            await _rewardRepository.CommitAsync();

            return _mapper.Map<RewardGetDto>(reward);
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new ArgumentException("Invalid ID");

            var reward = await _rewardRepository.GetByIdAsync(id);
            if (reward == null) throw new Exception("Reward not found.");

            _rewardRepository.Delete(reward);
            await _rewardRepository.CommitAsync();
        }

        public async Task<ICollection<RewardGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<Reward, bool>>? expression = null, params string[] includes)
        {
            var rewards = await _rewardRepository.GetByExpression(asnotracking, expression, includes).ToListAsync();

            return _mapper.Map<ICollection<RewardGetDto>>(rewards);
        }

        public async Task<RewardGetDto> GetById(int id)
        {
            if (id < 1) throw new Exception();

            var reward = await _rewardRepository.GetByIdAsync(id);
            if (reward == null) throw new Exception("Reward not found");

            return _mapper.Map<RewardGetDto>(reward);
        }

        public async Task<RewardGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<Reward, bool>>? expression = null, params string[] includes)
        {
            var reward = await _rewardRepository.GetByExpression(asnotracking, expression, includes).FirstOrDefaultAsync();
            if (reward == null) throw new Exception("Reward not found");

            return _mapper.Map<RewardGetDto>(reward);
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id < 1) throw new Exception();

            var reward = await _rewardRepository.GetByIdAsync(id);
            if (reward == null) throw new Exception("Reward not found.");

            reward.IsDeleted = true;

            await _rewardRepository.CommitAsync();
        }

        //public async Task UpdateAsync(int? id, RewardUpdateDto dto)
        //{
        //    if (id < 1 || id is null) throw new NullReferenceException("id is invalid");

        //    var reward = await _rewardRepository.GetByIdAsync((int)id);
        //    if (reward == null) throw new Exception("Reward not found");

        //    _mapper.Map(dto, reward);

        //    reward.UpdatedDate = DateTime.Now;

        //    await _rewardRepository.CommitAsync();
        //}
    }
}
