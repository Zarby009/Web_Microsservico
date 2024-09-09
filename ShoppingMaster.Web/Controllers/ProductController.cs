using Microsoft.AspNetCore.Mvc;
using ShoppingMaster.Web.Models;
using ShoppingMaster.Web.Services.Product;

namespace ShoppingMaster.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductInterface _productInterface;
        public ProductController(IProductInterface productInterface)
        {
            _productInterface = productInterface ?? throw new ArgumentNullException(nameof(productInterface));
        }


        public async Task<IActionResult> ProductIndex()
        {
            List<ProductModel> products = await _productInterface.FindAllAsync();
            return View(products);
        }
    }
}
