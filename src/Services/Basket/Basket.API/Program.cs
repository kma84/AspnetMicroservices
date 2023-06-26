using Basket.API.GrpcServices;
using Basket.API.Repositorioes;
using Discount.Grpc.Protos;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
string redisConnStr = configuration.GetSection("CacheSettings")["ConnectionString"] ?? string.Empty;
string discountUrl = configuration.GetSection("GrpcSettings")["DiscountUrl"] ?? string.Empty;

// Add services to the container.
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnStr;
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options => 
{ 
    options.Address = new Uri(discountUrl);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
