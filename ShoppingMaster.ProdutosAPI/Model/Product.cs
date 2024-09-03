using ShoppingMaster.ProdutosAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ShoppingMaster.ProdutosAPI.Model



{
    [Table("Product")]
    public class Product : BaseEntity
    {
        [Required]
        [Column("Name")]
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Preço tem que ser entre 1 e 10000")]
        [Column("Price")]
        [DataType(DataType.Currency)] // valor moeda
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Column("Description")]
        [StringLength(400, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 200 characters.")]
        [DataType(DataType.MultilineText)] // Representa texto de várias linhas.
        [Display(Name = "Product Description")]
        public string Description { get; set; }



        [Column("Category_Name")]
        [StringLength(50)]
        public string Category { get; set; }

        [Column("Image_URL")]
        [StringLength(300)]
        public string Url { get; set; }



    }

}
