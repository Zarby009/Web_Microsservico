﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingMaster.ProdutosAPI.Data;

#nullable disable

namespace ShoppingMaster.ProdutosAPI.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240905203858_UpdateBanco")]
    partial class UpdateBanco
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ShoppingMaster.ProdutosAPI.Model.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Category_Name");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)")
                        .HasColumnName("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Price");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("Image_URL");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
