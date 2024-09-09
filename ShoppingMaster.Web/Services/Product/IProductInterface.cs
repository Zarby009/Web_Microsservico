using ShoppingMaster.Web.Models;

namespace ShoppingMaster.Web.Services.Product
{
    public interface IProductInterface
    {
        Task<List<ProductModel>> FindAllAsync();
        Task<ProductModel> FindByIdAsync(long id);
        
        Task<ProductModel> Create(ProductModel productModel);
        Task<ProductModel> Update(ProductModel productModel);
        Task<bool> Delete(long id);

    }
}
