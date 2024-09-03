using Microsoft.EntityFrameworkCore;
using ShoppingMaster.ProdutosAPI.Model;
using ShoppingMaster.ProdutosAPI.Model.Base;
namespace ShoppingMaster.ProdutosAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }


    }
}