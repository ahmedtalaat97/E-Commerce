﻿using AutoMapper;
using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Enities;
using E_Commerce_Core.Enities.Basket;

namespace E_Commerce.API.Helper
{
    public class MappingProfile :Profile
    {

        public MappingProfile()
        {
            CreateMap<ProductBrand, BrandTypeDto>();
            CreateMap<ProductType, BrandTypeDto>();

            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.BrandName,o=>o.MapFrom(s=>s.ProductBrand.Name))
          .ForMember(d => d.TypeName, o => o.MapFrom(s => s.ProductType.Name))
          .ForMember(d=>d.PictureUrl,o=>o.MapFrom<PictureUrlResolver>());



            CreateMap<CustomerBasket,BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }

    }
}
