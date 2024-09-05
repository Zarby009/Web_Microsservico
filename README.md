
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










