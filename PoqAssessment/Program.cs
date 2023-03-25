using AspNetCoreRateLimit;
using PoqAssessment.Clients;
using PoqAssessment.Configurations;
using PoqAssessment.Interfaces;
using PoqAssessment.Middlewares;
using PoqAssessment.Options;
using PoqAssessment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ProductsClientOptions>(builder.Configuration.GetSection("ProductsClient"));
builder.Services.Configure<HighlightTagsOptions>(builder.Configuration.GetSection("HighlightTags"));
builder.Services.Configure<MostCommonWordsOptions>(builder.Configuration.GetSection("CommonWordsCount"));

builder.Services.AddProductsClientDI();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();
builder.Services.ConfigureRateLimit();

builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IServiceHandler, ServiceHandler>();

var corsPolicyName = "corsPolicy";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(corsPolicyName, builder =>
    {
        builder.WithOrigins("http://localhost:7076").AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<CustomClientRateLimitMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
