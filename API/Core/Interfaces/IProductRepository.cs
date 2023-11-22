using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        Task <IReadOnlyList <Product>> GetProductsAsync ();
        
        Task <Product> GetProductByIdAsync(int id);

        Task <IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

        Task <IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}