using AutoMapper;
using LoyaltyConsole.Business.DTOs.CashbackBalanceDtos;
using System.Linq.Expressions;
using LoyaltyConsole.Business.Interfaces;
using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyConsole.Business.Implementations
{
    public class CashbackBalanceService : ICashbackBalanceService
    {
        private readonly ICashbackBalanceRepository _cashbackBalanceRepository;
        private readonly IMapper _mapper;

        public CashbackBalanceService(ICashbackBalanceRepository cashbackBalanceRepository, IMapper mapper)
        {
            _cashbackBalanceRepository = cashbackBalanceRepository;
            _mapper = mapper;
        }

        public Task<bool> IsExist(Expression<Func<CashbackBalance, bool>> expression)
        {
            return _cashbackBalanceRepository.Table.AnyAsync(expression);
        }

        public async Task<CashbackBalanceGetDto> CreateAsync(CashbackBalanceCreateDto dto)
        {
            var cashbackBalance = _mapper.Map<CashbackBalance>(dto);
            cashbackBalance.CreatedDate = DateTime.Now;
            cashbackBalance.UpdatedDate = DateTime.Now;

            await _cashbackBalanceRepository.CreateAsync(cashbackBalance);
            await _cashbackBalanceRepository.CommitAsync();

            return _mapper.Map<CashbackBalanceGetDto>(cashbackBalance);
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new ArgumentException("Invalid ID");

            var cashbackBalance = await _cashbackBalanceRepository.GetByIdAsync(id);
            if (cashbackBalance == null) throw new Exception("CashbackBalance not found.");

            _cashbackBalanceRepository.Delete(cashbackBalance);
            await _cashbackBalanceRepository.CommitAsync();
        }


        public async Task<ICollection<CashbackBalanceGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<CashbackBalance, bool>>? expression = null, params string[] includes)
        {
            var cashbackBalances = await _cashbackBalanceRepository.GetByExpression(asnotracking, expression, includes).ToListAsync();

            return _mapper.Map<ICollection<CashbackBalanceGetDto>>(cashbackBalances);
        }

        public async Task<CashbackBalanceGetDto> GetById(int id)
        {
            if (id < 1) throw new Exception();

            var cashbackBalance = await _cashbackBalanceRepository.GetByIdAsync(id);
            if (cashbackBalance == null) throw new Exception("CashbackBalance not found");

            return _mapper.Map<CashbackBalanceGetDto>(cashbackBalance);
        }

        public async Task<CashbackBalanceGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<CashbackBalance, bool>>? expression = null, params string[] includes)
        {
            var cashbackBalance = await _cashbackBalanceRepository.GetByExpression(asnotracking, expression, includes).FirstOrDefaultAsync();
            if (cashbackBalance == null) throw new Exception("CashbackBalance not found");

            return _mapper.Map<CashbackBalanceGetDto>(cashbackBalance);
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id < 1) throw new Exception();

            var cashbackBalance = await _cashbackBalanceRepository.GetByIdAsync(id);
            if (cashbackBalance == null) throw new Exception("CashbackBalance not found.");

            cashbackBalance.IsDeleted = true;

            await _cashbackBalanceRepository.CommitAsync();
        }

        public async Task UpdateAsync(int? id, CashbackBalanceUpdateDto dto)
        {
            if (id < 1 || id is null) throw new NullReferenceException("id is invalid");

            var cashbackBalance = await _cashbackBalanceRepository.GetByIdAsync((int)id);
            if (cashbackBalance == null) throw new Exception("CashbackBalance not found");

            _mapper.Map(dto, cashbackBalance);

            cashbackBalance.UpdatedDate = DateTime.Now;

            await _cashbackBalanceRepository.CommitAsync();
        }
    }
}
