using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObjects;

namespace Service.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        #region Product

        CreateMap<Product, ProductDto>()
            .ForMember(
                m => m.BrandName,
                options => options.MapFrom(s => s.ProductBrand.Name)
            )
            .ForMember(m => m.TypeName,
                options => options.MapFrom(s => s.ProductType.Name));

        #endregion

        #region ProductType

        CreateMap<ProductType, TypeDto>();

        #endregion

        #region ProductBrand

        CreateMap<ProductBrand, BrandDto>();

        #endregion
    }
}