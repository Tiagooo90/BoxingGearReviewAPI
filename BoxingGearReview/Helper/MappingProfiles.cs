using AutoMapper;
using BoxingGearReview.Dto;
using BoxingGearReview.Models;

namespace BoxingGearReview.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<Equipment, EquipmentDto>();
            CreateMap<Brand, BrandDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Review, ReviewDto>();

            CreateMap<UserDto, User>();
            CreateMap<CategoryDto, Category>();
            CreateMap<BrandDto, Brand>();
            CreateMap<EquipmentDto, Equipment>();
            CreateMap<ReviewDto, Review>();

            {
                CreateMap<Equipment, EquipmentDto>()
                    .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

                CreateMap<Brand, BrandDto>();
                CreateMap<Category, CategoryDto>();
            }

            {
                CreateMap<ReviewDto, Review>()
                    .ForMember(dest => dest.User, opt => opt.Ignore()) // Ignorar User no mapeamento
                    .ForMember(dest => dest.Equipment, opt => opt.Ignore()); // Ignorar Equipment no mapeamento
            }

      
        }
    }
}
