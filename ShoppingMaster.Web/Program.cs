using ShoppingMaster.Web.Services.Product;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Adicione esta linha se você quiser garantir que a configuração é carregada
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


var serviceUrls = builder.Configuration.GetSection("ServiceUrls");
var productApiUrl = serviceUrls.GetValue<string>("ProductAPI");
builder.Services.AddHttpClient<IProductInterface, ProductService>(c =>
 c.BaseAddress = new Uri(productApiUrl)

) ;
builder.Services.AddScoped<IProductInterface, ProductService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
