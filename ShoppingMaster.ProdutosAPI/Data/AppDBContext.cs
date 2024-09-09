using global::ShoppingMaster.ProdutosAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ShoppingMaster.ProdutosAPI.Data
{


    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                // Tablets
                new Product
                {
                    Id = 6,
                    Category = "Tablets",
                    Description = "Tablet muito potente",
                    Name = "Tablet APi12",
                    Price = new decimal(3200.12),
                    Url = "https://via.placeholder.com/150?text=Tablet+APi12"
                },

                // Roupas
                new Product
                {
                    Id = 7,
                    Category = "Roupas",
                    Description = "Camisa de algodão de alta qualidade",
                    Name = "Camisa Casual Azul",
                    Price = new decimal(89.99),
                    Url = "https://via.placeholder.com/150?text=Camisa+Casual+Azul"
                },
                new Product
                {
                    Id = 8,
                    Category = "Roupas",
                    Description = "Calça jeans com corte moderno",
                    Name = "Calça Jeans Slim",
                    Price = new decimal(129.90),
                    Url = "https://via.placeholder.com/150?text=Calca+Jeans+Slim"
                },

                // Celulares
                new Product
                {
                    Id = 9,
                    Category = "Celulares",
                    Description = "Smartphone com 8GB de RAM e câmera de 64MP",
                    Name = "Smartphone X1000",
                    Price = new decimal(2999.99),
                    Url = "https://via.placeholder.com/150?text=Smartphone+X1000"
                },
                new Product
                {
                    Id = 10,
                    Category = "Celulares",
                    Description = "Smartphone com 5G e tela AMOLED",
                    Name = "Smartphone ZPro",
                    Price = new decimal(3499.49),
                    Url = "https://via.placeholder.com/150?text=Smartphone+ZPro"
                },

                // Cabos
                new Product
                {
                    Id = 11,
                    Category = "Cabos",
                    Description = "Cabo USB-C de 1 metro, rápido e durável",
                    Name = "Cabo USB-C Rápido",
                    Price = new decimal(29.99),
                    Url = "https://via.placeholder.com/150?text=Cabo+USB-C"
                },
                new Product
                {
                    Id = 12,
                    Category = "Cabos",
                    Description = "Cabo HDMI 4K de 2 metros",
                    Name = "Cabo HDMI 4K",
                    Price = new decimal(49.99),
                    Url = "https://via.placeholder.com/150?text=Cabo+HDMI+4K"
                },

                // Carros
                new Product
                {
                    Id = 13,
                    Category = "Carros",
                    Description = "Sedan esportivo com motor turbo",
                    Name = "Sedan Turbo GT",
                    Price = new decimal(49999.00),
                    Url = "https://via.placeholder.com/150?text=Sedan+Turbo+GT"
                },
                new Product
                {
                    Id = 14,
                    Category = "Carros",
                    Description = "SUV com tração integral e interior de couro",
                    Name = "SUV Luxury X",
                    Price = new decimal(69999.00),
                    Url = "https://via.placeholder.com/150?text=SUV+Luxury+X"
                }
            );
        }


    }
}


