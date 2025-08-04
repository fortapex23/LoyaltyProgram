using AutoMapper;
using LoyaltyConsole.Business.DTOs.UserDtos;
using LoyaltyConsole.Core.Models;

namespace LoyaltyConsole.Business.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<UserGetDto, AppUser>().ReverseMap();

            //CreateMap<CreateProductDTO, Product>().ReverseMap();
            //CreateMap<CreateProductDTO, Product>().ReverseMap();
            //CreateMap<GetProductDTO, Product>().ReverseMap();
        }
    }
}
