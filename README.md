
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
# Adicionar AutoMapper à API

## Por que usar AutoMapper?

Ao trabalhar com APIs que utilizam DTOs, é comum ter que mapear dados entre as entidades do banco de dados e os DTOs manualmente. O **AutoMapper** simplifica esse processo, automatizando o mapeamento entre objetos de diferentes tipos. Com isso, reduzimos a repetição de código, aumentamos a consistência e facilitamos a manutenção da aplicação.

### Vantagens de Usar AutoMapper

- **Redução de Código Manual**: Automatiza a conversão entre entidades e DTOs.
- **Facilidade de Manutenção**: Centraliza as regras de mapeamento, facilitando futuras alterações no código.
- **Consistência**: Garante que todos os mapeamentos sejam feitos de forma consistente em toda a aplicação.

### Configuração do AutoMapper

Para configurar o AutoMapper no projeto, adicione o seguinte código na classe `Program.cs` para registrar o AutoMapper como um serviço:

```csharp
IMapper mapper = ConfigMapping.RegisterMaps().CreateMapper(); //classe e método que iremos criar
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(typeof(ConfigMapping).Assembly);
```

A classe `ConfigMapping` é onde você define como as entidades e os DTOs serão mapeados:
```csharp
public class ConfigMapping : Profile
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(
            config =>
            {
                config.CreateMap<ProductDTO, Product>(); // Product = Entidade = Pegar do banco
                config.CreateMap<Product, ProductDTO>(); // ProductDTO = Retornar para a response
            }
            );
        return mappingConfig;
    }
}
```

No serviço ProductService, utilizamos o AutoMapper para mapear as entidades Product para os DTOs ProductDTO e vice-versa. Isso reduz o código necessário para a conversão de objetos e torna o serviço mais limpo.

## Exemplo de Uso no Serviço
No serviço `ProductService`, utilizamos o AutoMapper para mapear as entidades Product para os DTOs ProductDTO e vice-versa. Isso reduz o código necessário para a conversão de objetos e torna o serviço mais limpo.


```csharp

    private readonly AppDBContext _context;
    private readonly IMapper _mapper;

    public ProductService(AppDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
```

## Mapeando Entidade para DTO
Ao buscar um produto no banco de dados, utilizamos o AutoMapper para converter a entidade Product em um ProductDTO, que será retornado pela API. Dessa forma, garantimos que apenas os dados relevantes sejam enviados para o cliente.
| De          | Para        |
|-------------|-------------|
| `Product`| `ProductDTO`   |

```csharp
var prod = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
// Mapeamos a entidade Product para ProductDTO
var productDTO = _mapper.Map<ProductDTO>(prod);
```
## Mapeando DTO para Entidade
Quando recebemos um ProductDTO válido em uma requisição, usamos o AutoMapper para converter esse DTO de volta para a entidade Product, permitindo que o objeto seja salvo no banco de dados. Isso facilita o processo de criação e atualização de produtos.
| De          | Para        |
|-------------|-------------|
| `ProductDTO`| `Product`   |

```csharp
Exemplo: recebemos um (ProductDTO productDTO) válido;
var product = _mapper.Map<Product>(productDTO);
```
Essas melhorias tornam o processo de mapeamento mais claro, destacando a função do AutoMapper tanto na conversão da entidade para o DTO quanto do DTO para a entidade, além de reforçar o papel de cada transformação no fluxo da aplicação.

# Fazendo o FrontEnd

## Inicio

primeiramente, atualizamos o bootstrap e colocamos o fontawesome no nosso codigo:
bootstrap: atualizado no Views/Shared/_Layout link no Head, script bundle no body nas últimas linhas.

fontawesome: script no Head

### Interfaces e Serviços


- **IProductInterface**: Onde fizemos os contratos para nossa service.
- **ProductInterface**: Onde assinamos os contratos e implementamos os métodos.

### No appsettings.json: 
adicionamos ServiceUrls:ProductAPI 

|   Nome   | Para quê?       |     Valor     |
|-------------|-------------|-------------------|
| `"ProductAPI"`| Url e a Porta da nossa api   | "https://localhost:7298" |

```json
{
  [...],
  "ServiceUrls": {
    "ProductAPI": "https://localhost:7298"
  }
}
```
### No `Program.cs` mapeamos o AddHttpClient:
pegamos esse valor para colocar no HttpClient e colocar IProductInterface na Injecao de dependencia para ProductService junto com o valor BaseAddress com o nosso {ProductApi}

```csharp
var serviceUrls = builder.Configuration.GetSection("ServiceUrls");
var productApiUrl = serviceUrls.GetValue<string>("ProductAPI");
builder.Services.AddHttpClient<IProductInterface, ProductService>(c =>
 c.BaseAddress = new Uri(productApiUrl)

) ;
```
# Para consumir API iremos usar esse seguinte código:
```csharp
var response = await _httpClient.GetAsync("[caminho da api]]");
var jsonString = await response.Content.ReadAsStringAsync();
var products = JsonSerializer.Deserialize<[modo de retorno da classe]>(jsonString, new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
});

```

# Criaremos uma Controller = ProductController
Injetando nossa ProductInterface e colocando no Construtor

Fazendo assim, a chamada do método que queremos e passando para a View.
```csharp
using Microsoft.AspNetCore.Mvc;
using ShoppingMaster.Web.Models;
using ShoppingMaster.Web.Services.Product;

namespace ShoppingMaster.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductInterface _productInterface;
        public ProductController(IProductInterface productInterface)
        {
            _productInterface = productInterface ?? throw new ArgumentNullException(nameof(productInterface));
        }


        public async Task<IActionResult> ProductIndex()
        {
            List<ProductModel> products = await _productInterface.FindAllAsync();
            return View(products);
        }
    }
}

```

# Após isso, criaremos nossa View Product
Iremos fazer o 
```csharp
@model List<ProductModel>

@item para acessar

```
