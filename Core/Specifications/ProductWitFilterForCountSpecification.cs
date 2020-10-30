using Core.Entities;
using Core.Specifications.pager;

namespace Core.Specifications
{
    public class ProductWitFilterForCountSpecification :BaseSpceification<Product>
    {
        public ProductWitFilterForCountSpecification(ProductSpecParams productSpec)
         :base(x=> 
                    (string.IsNullOrEmpty(productSpec.Search) || x.Name.ToLower().Contains(productSpec.Search))&&
                    (!productSpec.BrandId.HasValue||x.ProductBrandId==productSpec.BrandId) &&
                    (!productSpec.TypeId.HasValue || x.ProductTypeId==productSpec.TypeId)
            )
        {
            
        }
    }
}