namespace ShoppingMaster.ProdutosAPI.DTO
{

    // Apenas um espelho da nossa classe Product para nao expor ela por completo pois é um mal uso
    public class ProductDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
    }
}
