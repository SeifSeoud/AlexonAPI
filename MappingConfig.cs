using AlexonTask.Models;
using AutoMapper;

namespace AlexonTask
{
    public class MappingConfig:Profile
    {
        public MappingConfig() {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
        }
    }
}
