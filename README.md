
# Código para fazer alterações no Banco
## Migration do Banco:
```bash
dotnet ef migrations add InicializandoBanco --project ShoppingMaster.ProdutosAPI --startup-project ShoppingMaster.ProdutosAPI
```
## Update do Banco de Dados
```bash
dotnet ef database update --project C:\Users\User\source\repos\ShoppingMaster\ShoppingMaster.ProdutosAPI\ShoppingMaster.ProdutosAPI.csproj --startup-project C:\Users\User\source\repos\ShoppingMaster\ShoppingMaster.ProdutosAPI\ShoppingMaster.ProdutosAPI.csproj
```
# Adicionar DTO/VO à API

## Por que usar DTO/VO?

Usar diretamente as entidades principais nas respostas da API pode expor dados sensíveis, causar acoplamento excessivo entre a API e o banco de dados, e limitar o controle sobre o formato dos dados. Para evitar esses problemas, utilizamos **DTO (Data Transfer Object)** ou **VO (Value Object)**.

### Vantagens de DTO/VO

- **Proteção de dados sensíveis**: Evita expor campos desnecessários.
- **Desacoplamento**: Mantém a estrutura da API separada da estrutura do banco.
- **Flexibilidade**: Permite moldar os dados para o formato correto.
  
### Exemplo de Uso 

Entidade `Produto`:

```C#
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public DateTime DataCriacao { get; set; }
    public bool Ativo { get; set; }
}
````

DTO `ProdutoDTO`
é o que vai retornar para a response:
````csharp
public class ProdutoDTO
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}
````
#### Uso na API
````csharp
[HttpGet]
public ActionResult<IEnumerable<ProdutoDTO>> GetProdutos()
{
    var produtos = _context.Produtos
        .Where(p => p.Ativo)
        .Select(p => new ProdutoDTO
        {
            Nome = p.Nome,
            Preco = p.Preco
        }).ToList();

    return Ok(produtos);
}
````
## DTO/VO No nosso Shopping

Entidade `Product` herdando de `BaseEntity`:

```C#
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
````
`BaseEntity`:
````csharp
public class BaseEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
}
````


DTO `ProductDTO`
é o que vai retornar para a response:
````csharp
public class ProductDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Url { get; set; }
}
````
#### Colocamos AddSingleton e AddAutoMapper na `Program.cs`
````csharp
IMapper mapper = ConfigMapping.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(typeof(ConfigMapping).Assembly);
````










