using AutoMapper;
using LoyaltyConsole.Business.DTOs.CashbackBalanceDtos;
using LoyaltyConsole.Business.DTOs.TransactionDtos;
using LoyaltyConsole.Business.DTOs.UserDtos;
using LoyaltyConsole.Core.Models;

namespace LoyaltyConsole.Business.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<UserGetDto, AppUser>().ReverseMap();

            CreateMap<TransactionCreateDto, Transaction>().ReverseMap();
            CreateMap<TransactionGetDto, Transaction>().ReverseMap();
            CreateMap<TransactionUpdateDto, Transaction>().ReverseMap();

            CreateMap<CashbackBalanceCreateDto, CashbackBalance>().ReverseMap();
            CreateMap<CashbackBalanceGetDto, CashbackBalance>().ReverseMap();
            CreateMap<CashbackBalanceUpdateDto, CashbackBalance>().ReverseMap(); 
        }
    }
}
