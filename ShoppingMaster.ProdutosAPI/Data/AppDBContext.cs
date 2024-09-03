﻿using global::ShoppingMaster.ProdutosAPI.Model;
using Microsoft.EntityFrameworkCore;

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


