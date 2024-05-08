using AutoMapper;
using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Enities;
using E_Commerce_Core.Interfaces.Repositories;
using E_Commerce_Core.Interfaces.Services;
using E_Commerce_Core.Specifications;
using E_Commerce_Repository.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandTypeDto>>(brands);

        }

        public async Task<PaginatedResultDto<ProductToReturnDto>> GetAllProductsAsync(ProductSpecificationParameters specParameter)
        {
          
            var specs=new ProductSpecifications(specParameter);
            var types = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(specs);
            var countSpec = new ProductCountWithSpec(specParameter);
            var count = await _unitOfWork.Repository<Product, int>().GetProductCountAsync(countSpec);
            var mappedProducts= _mapper.Map<IReadOnlyList<ProductToReturnDto>>(types);

            return new PaginatedResultDto<ProductToReturnDto>
            {
                Data = mappedProducts,
                PageIndex=specParameter.PageIndex,
                PageSize=specParameter.PageSize,
                TotalCount=count,

            };

        }

        public async Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandTypeDto>>(types);
        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var specs=new ProductSpecifications(id);
            var products = await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(specs);
            return _mapper.Map<ProductToReturnDto>(products);
        }
    }
}
