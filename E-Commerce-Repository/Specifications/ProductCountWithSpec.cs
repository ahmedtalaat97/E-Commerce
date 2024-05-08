using E_Commerce_Core.Enities;
using E_Commerce_Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Repository.Specifications
{
    public class ProductCountWithSpec : BaseSpecifications<Product>
    {
        public ProductCountWithSpec(ProductSpecificationParameters specificationParameters) :
          base(product => (!specificationParameters.BrandId.HasValue || product.Id == specificationParameters.BrandId.Value)
         && (!specificationParameters.TypeId.HasValue || product.Id == specificationParameters.TypeId.Value)
            && (string.IsNullOrWhiteSpace(specificationParameters.Search) || product.Name.ToLower().Contains(specificationParameters.Search)))
        {
        }
    }
}
