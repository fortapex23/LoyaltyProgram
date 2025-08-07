using AutoMapper;
using LoyaltyConsole.Business.DTOs.AppUserRewardDtos;
using LoyaltyConsole.Business.DTOs.CashbackBalanceDtos;
using LoyaltyConsole.Business.DTOs.CustomerTagDtos;
using LoyaltyConsole.Business.DTOs.RewardDtos;
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

            CreateMap<RewardCreateDto, Reward>().ReverseMap();
            CreateMap<RewardGetDto, Reward>().ReverseMap();
            
            CreateMap<CustomerTagCreateDto, CustomerTag>().ReverseMap();
            CreateMap<CustomerTagGetDto, CustomerTag>().ReverseMap();
            CreateMap<CustomerTagUpdateDto, CustomerTag>().ReverseMap();

            CreateMap<CashbackBalanceCreateDto, CashbackBalance>().ReverseMap();
            CreateMap<CashbackBalanceGetDto, CashbackBalance>().ReverseMap();
            CreateMap<CashbackBalanceUpdateDto, CashbackBalance>().ReverseMap(); 

            CreateMap<AppUserRewardCreateDto, AppUserReward>().ReverseMap();
            CreateMap<AppUserRewardGetDto, AppUserReward>().ReverseMap();
            CreateMap<AppUserRewardUpdateDto, AppUserReward>().ReverseMap();
        }
    }
}
