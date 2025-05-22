using AutoMapper;
using Entities;
using Shared;

namespace ProductCatalogAPI
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.UserName));
        
            CreateMap<ProductForCreationDto, Product>();
         
            CreateMap<ProductForUpdateDto, Product>();

            //CreateMap<UserForRegistrationDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserForRegistrationDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
    
}

