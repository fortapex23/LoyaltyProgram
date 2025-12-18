using AutoMapper;
using LoyaltyConsole.Business.DTOs.CashbackBalanceDtos;
using LoyaltyConsole.Business.DTOs.CustomerDtos;
using LoyaltyConsole.Business.DTOs.CustomerImageDtos;
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

            CreateMap<CustomerCreateDto, Customer>().ReverseMap();
            CreateMap<CustomerUpdateDto, Customer>().ReverseMap();
            CreateMap<Customer, CustomerGetDto>()
                .ForMember(dest => dest.CustomerImage, opt =>
                opt.MapFrom(src => src.CustomerImage != null
                ? new CustomerImageGetDto(src.CustomerImage.ImageUrl)
                : null));
            CreateMap<Customer, CustomerListDto>()
                .ForMember(dest => dest.TotalCashback,
                    opt => opt.MapFrom(src => src.CashbackBalance != null
                        ? src.CashbackBalance.TotalCashback
                        : 0))
                .ForMember(dest => dest.CashbackAvailable,
                    opt => opt.MapFrom(src => src.CashbackBalance != null
                        ? src.CashbackBalance.CashbackAvailable
                        : 0))
                .ForMember(dest => dest.CashbackRedeemed,
                    opt => opt.MapFrom(src => src.CashbackBalance != null
                        ? src.CashbackBalance.CashbackRedeemed
                        : 0))
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.CustomerImage != null
                        ? src.CustomerImage.ImageUrl
                        : null));


            CreateMap<CustomerImageGetDto, CustomerImage>().ReverseMap();
        }
    }
}
