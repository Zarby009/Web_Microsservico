using ShoppingMaster.ProdutosAPI.DTO;

namespace ShoppingMaster.ProdutosAPI.Services.ProductRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> FindAllAsync();
        Task<ProductDTO> FindByIdAsync(long id);
        Task<ProductDTO> Create(ProductDTO productDTO);
        Task<ProductDTO> Update(ProductDTO productDTO);
        Task<bool> Delete(long id);



    }
}
