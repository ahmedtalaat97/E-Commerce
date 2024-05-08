using E_Commerce_Core.Enities;
using E_Commerce_Core.Interfaces;
using E_Commerce_Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Repository.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product>
    {
        //get product with filtiration
        public ProductSpecifications(ProductSpecificationParameters specs) :
            base
            (product=>(!specs.BrandId.HasValue||product.Id==specs.BrandId.Value)
            &&(!specs.TypeId.HasValue || product.Id == specs.TypeId.Value)
            &&(string.IsNullOrWhiteSpace(specs.Search)||product.Name.ToLower().Contains(specs.Search)))
        {
            IncludeExpressions.Add(product => product.ProductBrand);

            IncludeExpressions.Add(product => product.ProductType);

            ApplyPagination(specs.PageSize, specs.PageIndex);

            if (specs.Sort is not null)

            {
                switch (specs.Sort)
                {
                    case ProductSortingParameters.NameAsc:
                        OrderBy = x=>x.Name;
                        break;

                    case ProductSortingParameters.NameDesc:
                        OrderByDesc = x => x.Name;
                        break;


                    case ProductSortingParameters.PriceAsc:
                        OrderBy = x => x.Price;
                        break;

                    case ProductSortingParameters.PriceDesc:
                        OrderByDesc= x => x.Price;
                        break;


                    default:
                        OrderBy = x => x.Name;
                        break;
                }

            }
            else
            {
                OrderBy = x => x.Name;
            }
        }
        //get product by id
        public ProductSpecifications(int id):base(product=>product.Id==id) 
        {
            IncludeExpressions.Add(product => product.ProductBrand);

            IncludeExpressions.Add(product => product.ProductType);
        }
    }
        
    

}
