using Microsoft.EntityFrameworkCore;
using ShoppingMaster.ProdutosAPI.Data;


// Inicializar o Banco de dados no CLI: dotnet ef migrations add InicializandoBanco --project ShoppingMaster.ProdutosAPI --startup-project ShoppingMaster.ProdutosAPI
// Fazer update no Banco: dotnet ef database update --project C:\Users\User\source\repos\ShoppingMaster\ShoppingMaster.ProdutosAPI\ShoppingMaster.ProdutosAPI.csproj --startup-project C:\Users\User\source\repos\ShoppingMaster\ShoppingMaster.ProdutosAPI\ShoppingMaster.ProdutosAPI.csproj

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>(
    options => options.UseMySQL(builder.Configuration.GetConnectionString("ConexaoPadrao"), optionsConfig => ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ConexaoPadrao")))

    );



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
