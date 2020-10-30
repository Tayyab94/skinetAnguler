using System.Threading.Tasks;
using Core.Entities;
using System.Collections.Generic;


namespace Core.Interfaces
{
    public interface IProductRepository
    {

        Task<Product> GetProductById(int id);
      
        Task<IReadOnlyList<Product>>GetProducts();
        

        Task<IReadOnlyList<ProdcutBrand>>GetProductBrandsAsyns();
        
        Task<IReadOnlyList<ProductType>>GetProductTypesAsyns();
    }
}