using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoppingMaster.ProdutosAPI.Migrations
{
    /// <inheritdoc />
    public partial class adicionando_produtos_ficticios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "id", "Category_Name", "Description", "Name", "Price", "Image_URL" },
                values: new object[,]
                {
                    { 6L, "Tablets", "Tablet muito potente", "Tablet APi12", 3200.12m, "https://via.placeholder.com/150?text=Tablet+APi12" },
                    { 7L, "Roupas", "Camisa de algodão de alta qualidade", "Camisa Casual Azul", 89.99m, "https://via.placeholder.com/150?text=Camisa+Casual+Azul" },
                    { 8L, "Roupas", "Calça jeans com corte moderno", "Calça Jeans Slim", 129.9m, "https://via.placeholder.com/150?text=Calca+Jeans+Slim" },
                    { 9L, "Celulares", "Smartphone com 8GB de RAM e câmera de 64MP", "Smartphone X1000", 2999.99m, "https://via.placeholder.com/150?text=Smartphone+X1000" },
                    { 10L, "Celulares", "Smartphone com 5G e tela AMOLED", "Smartphone ZPro", 3499.49m, "https://via.placeholder.com/150?text=Smartphone+ZPro" },
                    { 11L, "Cabos", "Cabo USB-C de 1 metro, rápido e durável", "Cabo USB-C Rápido", 29.99m, "https://via.placeholder.com/150?text=Cabo+USB-C" },
                    { 12L, "Cabos", "Cabo HDMI 4K de 2 metros", "Cabo HDMI 4K", 49.99m, "https://via.placeholder.com/150?text=Cabo+HDMI+4K" },
                    { 13L, "Carros", "Sedan esportivo com motor turbo", "Sedan Turbo GT", 49999m, "https://via.placeholder.com/150?text=Sedan+Turbo+GT" },
                    { 14L, "Carros", "SUV com tração integral e interior de couro", "SUV Luxury X", 69999m, "https://via.placeholder.com/150?text=SUV+Luxury+X" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 14L);
        }
    }
}
