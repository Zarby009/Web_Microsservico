using AutoMapper;
using ShoppingMaster.ProdutosAPI.DTO;
using ShoppingMaster.ProdutosAPI.Model;

namespace ShoppingMaster.ProdutosAPI.Config
{
    public class ConfigMapping : Profile
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(
                config =>
                {
                    config.CreateMap<ProductDTO, Product>(); // Product = Entidade = Pegar do banco
                    config.CreateMap<Product, ProductDTO>(); // ProductDTO = Retornar para a response
                }
                );
            return mappingConfig;
        }
    }
}