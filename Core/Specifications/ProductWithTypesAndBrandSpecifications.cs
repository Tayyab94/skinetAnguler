using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications.pager;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandSpecifications : BaseSpceification<Product>
    {
        public ProductWithTypesAndBrandSpecifications(ProductSpecParams productSpecs)
            :base(x=> 
                        // This is For searching...
                    (string.IsNullOrEmpty(productSpecs.Search) || x.Name.ToLower().Contains(productSpecs.Search))&&
            
                    (!productSpecs.BrandId.HasValue||x.ProductBrandId==productSpecs.BrandId) &&
            
                    (!productSpecs.TypeId.HasValue || x.ProductTypeId==productSpecs.TypeId)
            )
        {
                AddIncludes(s=>s.ProductBrand);
                AddIncludes(s=>s.ProductType);
                AddOrderBy(s=>s.Name);

             AddPaging(productSpecs.PageSize *(productSpecs.PageIndex-1),productSpecs.PageSize);
              //  AddPaging(productSpecs.PageSize,productSpecs.PageSize *(productSpecs.PageIndex -1));
                if(!string.IsNullOrEmpty(productSpecs.Sort))
                {
                    switch(productSpecs.Sort)
                    {
                        case "priceAsce":
                            AddOrderBy(p=>p.Price);
                            break;

                        case "priceDesc":
                            AddOrderByDescending(p=>p.Price);
                            break;    
                        default:
                            AddOrderBy(s=>s.Name);
                            break;
                    }
                }
        }

        public ProductWithTypesAndBrandSpecifications(int id):base(s=>s.Id==id)
        {
             AddIncludes(s=>s.ProductBrand);
                AddIncludes(s=>s.ProductType);

        
        }
    }
}