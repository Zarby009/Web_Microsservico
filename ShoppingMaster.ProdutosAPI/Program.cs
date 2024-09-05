using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShoppingMaster.ProdutosAPI.Config;
using ShoppingMaster.ProdutosAPI.Data;
using ShoppingMaster.ProdutosAPI.Services.ProductRepository;



// Inicializar o Banco de dados no CLI: dotnet ef migrations add InicializandoBanco --project ShoppingMaster.ProdutosAPI --startup-project ShoppingMaster.ProdutosAPI
// Fazer update no Banco: dotnet ef database update --project C:\Users\User\source\repos\ShoppingMaster\ShoppingMaster.ProdutosAPI\ShoppingMaster.ProdutosAPI.csproj --startup-project C:\Users\User\source\repos\ShoppingMaster\ShoppingMaster.ProdutosAPI\ShoppingMaster.ProdutosAPI.csproj

var builder = WebApplication.CreateBuilder(args);

IMapper mapper = ConfigMapping.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(typeof(ConfigMapping).Assembly);

// Add services to the container.
builder.Services.AddScoped<IProductInterface, ProductService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
    
    );
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
