using AutoMapper;
using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Enities;

namespace E_Commerce.API.Helper
{
    public class PictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context) 
            =>
            !string.IsNullOrWhiteSpace(source.PictureUrl) ? $"{_configuration["BaseUrl"]}{source.PictureUrl}" : string.Empty;
    }
}
