using AdminDashboard.Models;
using AutoMapper;
using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Enities;

namespace AdminDashboard.Helpers
{
    public class ProductMappingProfile :Profile
    {

        public ProductMappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();


            CreateMap<Product, ProductToReturnDto>()
               .ForMember(d => d.BrandName, o => o.MapFrom(s => s.ProductBrand.Name))
         .ForMember(d => d.TypeName, o => o.MapFrom(s => s.ProductType.Name));



		}


    }
}
