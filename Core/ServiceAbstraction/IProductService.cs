using Shared.DataTransferObjects;

namespace ServiceAbstraction;

public interface IProductService
{
    // GetAll
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();

    // GetById
    Task<ProductDto> GetProductByIdAsync(int id);

    // GetAllTypes
    Task<IEnumerable<TypeDto>> GetAllTypesAsync();

    //GetAllBrands
    Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
}