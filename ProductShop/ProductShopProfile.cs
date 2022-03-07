using AutoMapper;

namespace ProductShop
{
   public class ProductShopProfile : Profile
   {
      public ProductShopProfile() //init automapper
      {
		CreateMap<UserInputDto, User>();
		
		CreateMap<ProductInputDto, Product>();
      }
   }
}
