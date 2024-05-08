using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.Interfaces.Services
{
    public interface IProductService
    {

        Task<PaginatedResultDto<ProductToReturnDto>> GetAllProductsAsync(ProductSpecificationParameters specificationParameters);
        Task<ProductToReturnDto> GetProductAsync(int id);
        Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync();

    }
}
