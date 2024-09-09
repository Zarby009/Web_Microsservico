using ShoppingMaster.Web.Models;
using ShoppingMaster.Web.Utils;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingMaster.Web.Services.Product
{
    public class ProductService : IProductInterface
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        

        public async Task<List<ProductModel>> FindAllAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7298/api/Product/GetAll");
            var jsonString = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<ProductModel>>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (products == null)
            {
                throw new Exception("Erro ao desserializar os produtos.");
            }


            return products;
        }

        public async Task<ProductModel> FindByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"api/Product/BuscarPorId/{id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> Create(ProductModel productModel)
        {
            var response = await _httpClient.PostAsJson("api/Product/Create", productModel);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();
            else
                throw new Exception("Erro no Create");
        }

        public async Task<ProductModel> Update(ProductModel productModel)
        {
            var response = await _httpClient.PutAsJson("api/Product/Atualizar", productModel);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();
            else
                throw new Exception("Erro no Update");
        }

        public async Task<bool> Delete(long id)
        {
            var response = await _httpClient.DeleteAsync($"api/Product/BuscarPorId/{id}");
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else
                throw new Exception("Erro ao deletar");
        }
    }
}
