using Basket.API.Repositorioes;

var builder = WebApplication.CreateBuilder(args);

string redisConnStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("CacheSettings")["ConnectionString"] ?? string.Empty;

// Add services to the container.
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnStr;
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

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
