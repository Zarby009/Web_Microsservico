using ShoppingMaster.ProdutosAPI.DTO;

namespace ShoppingMaster.ProdutosAPI.Services.ProductRepository
{
    public interface IProductInterface
    {
        Task<List<ProductDTO>> FindAllAsync();
        Task<ProductDTO> FindByIdAsync(long id);
        Task<ProductDTO> Create(ProductDTO productDTO);
        Task<ProductDTO> Update(ProductDTO productDTO);
        Task<bool> Delete(long id);



    }
}
