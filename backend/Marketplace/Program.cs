using Marketplace.Repositories;
using Marketplace.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositorios
builder.Services.AddSingleton<UsuarioRepository>();
builder.Services.AddSingleton<ProductoRepository>();
builder.Services.AddSingleton<PublicacionRepository>();
builder.Services.AddSingleton<CompraRepository>();
builder.Services.AddSingleton<CampaniaRepository>();
builder.Services.AddSingleton<EnvioRepository>();

// Servicios
builder.Services.AddScoped<MarketplaceService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();