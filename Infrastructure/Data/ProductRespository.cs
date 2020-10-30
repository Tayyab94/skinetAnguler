using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data
{
    public class ProductRespository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRespository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Include(s=>s.ProductBrand).Include(s=>s.ProductType)
            .FirstOrDefaultAsync(sb=>sb.Id.Equals(id));
        }

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
        
            return await _context.Products.Include(s=>s.ProductBrand).Include(s=>s.ProductType).ToListAsync();
        }


        public async Task AddData()
        {
              var productsData=File.ReadAllText("../Infrastructure/Data/seedData/products.json");

                        var products=JsonSerializer.Deserialize<List<Product>>(productsData);

                        foreach(var pro in products)
                        {
                            _context.Products.Add(pro);

                        }

                        await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<ProdcutBrand>> GetProductBrandsAsyns()
        {
                    return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsyns()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}