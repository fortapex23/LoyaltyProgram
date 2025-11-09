using LoyaltyConsole.Business.DTOs.CashbackBalanceDtos;
using LoyaltyConsole.Business.DTOs.CustomerImageDtos;
using LoyaltyConsole.Business.DTOs.TransactionDtos;
using Microsoft.AspNetCore.Http;

namespace LoyaltyConsole.Business.DTOs.CustomerDtos
{
    public record CustomerGetDto(int Id, string FullName, DateTime Birthday, CashbackBalanceGetDto CashbackBalance, CustomerImageGetDto? CustomerImage,
                            ICollection<TransactionGetDto> Transactions, DateTime CreatedDate);
}
