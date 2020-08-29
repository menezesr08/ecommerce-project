using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        // we added async as a naming convention to show its async method and needs to be awaited
        Task<Product> GetProductByIdAsync(int id);
        /* IReadOnlyList is a specfic type of list and has limited functionality compared to a normal list
         In a normal list you can add and remove items but we don't really need it here so we can be specific
         and use IReadOnlyList */
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}