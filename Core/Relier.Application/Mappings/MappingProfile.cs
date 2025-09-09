using AutoMapper;
using Relier.Application.DTOs;
using Relier.Domain.Entities;

namespace Relier.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
