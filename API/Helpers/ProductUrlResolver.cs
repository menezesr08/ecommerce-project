using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    /*
    this class allows us to resolve picture url so that we can set the pictureurl to show the url from the place where
    the image is being served from. Example is: http://localhost:5001/urlofImage
    The interface takes in 3 parameters. 
    Source: Product
    Destination: ProductToReturnDto
    string: this is new url that will be returned
    */
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }
            //In case picture url is null but it should not be as we've configured this property to never be null in EF
            return null;
        }
    }
}