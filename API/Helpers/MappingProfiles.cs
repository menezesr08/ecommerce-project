using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            /*
            Here we are mapping the attributes to two classes.
            However ProductBrand and ProductType are classes in Product and we just want their names
            in ProductToReturnDto.
            So We need to configure these attributes using ForMember so that we can retrieve the name from
            the class and map it to the ProductBrand string in the producttoreturndto.
            */
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}