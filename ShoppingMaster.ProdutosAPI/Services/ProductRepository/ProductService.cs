using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore; // using System.Data.Entity; é para o EF6
using ShoppingMaster.ProdutosAPI.Data;
using ShoppingMaster.ProdutosAPI.DTO;
using ShoppingMaster.ProdutosAPI.Model;

namespace ShoppingMaster.ProdutosAPI.Services.ProductRepository
{
    public class ProductService : IProductInterface
    {

        private AppDBContext _context;
        private IMapper _mapper;

        public ProductService(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<ProductDTO>> FindAllAsync()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductDTO> Create(ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                throw new ArgumentNullException(nameof(productDTO), "Os dados do produto tem que ser válidos");
            }

            var prod = _mapper.Map<Product>(productDTO);
            // Adiciona a entidade ao contexto
            await _context.Products.AddAsync(prod);
            await _context.SaveChangesAsync();

            // Mapeia a entidade salva de volta para o DTO
            return _mapper.Map<ProductDTO>(prod);

        }

        public async Task<bool> Delete(long id)
        {
            var prod = _context.Products.FirstOrDefault(p => p.Id == id);
            if (prod == null) { return false; };
            _context.Products.Remove(prod);
            await _context.SaveChangesAsync();
            return true;

        }


        public async Task<ProductDTO> FindByIdAsync(long id)
        {
            Product prod = await _context.Products.FirstOrDefaultAsync(p => p.Id == id) ?? new Product();
            return _mapper.Map<ProductDTO>(prod);


        }

        public async Task<ProductDTO> Update(ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                throw new ArgumentNullException(nameof(productDTO), "O DTO do produto não pode ser nulo.");
            }

            // Recupera o produto existente com base no ID
            Product prod = await _context.Products.FindAsync(productDTO.Id);

            // Se o produto não for encontrado, lança uma exceção
            if (prod == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            // Verifica e aplica apenas as mudanças necessárias
            var isUpdated = false;

            if (prod.Description != productDTO.Description)
            {
                prod.Description = productDTO.Description;
                isUpdated = true;
            }

            if (prod.Price != productDTO.Price)
            {
                prod.Price = productDTO.Price;
                isUpdated = true;
            }

            if (prod.Url != productDTO.Url)
            {
                prod.Url = productDTO.Url;
                isUpdated = true;
            }

            if (prod.Category != productDTO.Category)
            {
                prod.Category = productDTO.Category;
                isUpdated = true;
            }

            // Se houver alterações, atualiza o produto e salva as mudanças
            if (isUpdated)
            {
                _context.Products.Update(prod);
                await _context.SaveChangesAsync();
            }

            return productDTO;
        }


    }
}
