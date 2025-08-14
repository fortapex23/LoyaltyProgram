using System.Linq.Expressions;
using AutoMapper;
using LoyaltyConsole.Business.DTOs.TransactionDtos;
using LoyaltyConsole.Business.ExternalServices.Interfaces;
using LoyaltyConsole.Business.Interfaces;
using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyConsole.Business.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICashbackBalanceRepository _cashbackBalanceRepository;
        private readonly ICashbackService _cashbackService;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper
            , ICashbackBalanceRepository cashbackBalanceRepository)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _cashbackBalanceRepository = cashbackBalanceRepository; 

        }

        public Task<bool> IsExist(Expression<Func<Transaction, bool>> expression)
        {
            return _transactionRepository.Table.AnyAsync(expression);
        }

        public async Task<TransactionGetDto> CreateAsync(TransactionCreateDto dto)
        {
            var transaction = _mapper.Map<Transaction>(dto);

            var cashbackRate = _cashbackService.GetCashbackRate(dto.Business);
            transaction.CashbackEarned = dto.AmountSpent * cashbackRate;
            transaction.CreatedDate = DateTime.Now;
            transaction.UpdatedDate = DateTime.Now;

            await _transactionRepository.CreateAsync(transaction);
            await _transactionRepository.CommitAsync();

            var balance = _cashbackBalanceRepository.GetByExpression(false, x => x.AppUserId == dto.AppUserId).FirstOrDefault();
            if (balance == null) throw new Exception("CashbackBalance not found");

            balance.TotalCashback += transaction.CashbackEarned;

            await _cashbackBalanceRepository.CommitAsync();

            return _mapper.Map<TransactionGetDto>(transaction);
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new ArgumentException("Invalid ID");

            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null) throw new Exception("Transaction not found.");

            _transactionRepository.Delete(transaction);
            await _transactionRepository.CommitAsync();
        }


        public async Task<ICollection<TransactionGetDto>> GetByExpression(bool asnotracking = false, Expression<Func<Transaction, bool>>? expression = null, params string[] includes)
        {
            var transactions = await _transactionRepository.GetByExpression(asnotracking, expression, includes).ToListAsync();

            return _mapper.Map<ICollection<TransactionGetDto>>(transactions);
        }

        public async Task<TransactionGetDto> GetById(int id)
        {
            if (id < 1) throw new Exception();

            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null) throw new Exception("Transaction not found");

            return _mapper.Map<TransactionGetDto>(transaction);
        }

        public async Task<TransactionGetDto> GetSingleByExpression(bool asnotracking = false, Expression<Func<Transaction, bool>>? expression = null, params string[] includes)
        {
            var transaction = await _transactionRepository.GetByExpression(asnotracking, expression, includes).FirstOrDefaultAsync();
            if (transaction == null) throw new Exception("Transaction not found");

            return _mapper.Map<TransactionGetDto>(transaction);
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id < 1) throw new Exception();

            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null) throw new Exception("Transaction not found.");

            transaction.IsDeleted = true;

            await _transactionRepository.CommitAsync();
        }

        public async Task UpdateAsync(int? id, TransactionUpdateDto dto)
        {
            if (id < 1 || id is null) throw new NullReferenceException("id is invalid");

            var transaction = await _transactionRepository.GetByIdAsync((int)id);
            if (transaction == null) throw new Exception("Transaction not found");

            _mapper.Map(dto, transaction);

            transaction.UpdatedDate = DateTime.Now;

            await _transactionRepository.CommitAsync();
        }

    }
}
