using IfoodParaguai;
using IfoodParaguai.Models;
using IfoodParaguai.Services;

var builder = WebApplication.CreateBuilder(args);
var TokenService = new TokenService();

//Configuração do data settings referente aos Models
builder.Services.Configure<UserDataSettings>(builder.Configuration.GetSection("UserStoreDatabase"));
builder.Services.Configure<ProdutoDataSettings>(builder.Configuration.GetSection("ProdutoStoreDatabase"));
builder.Services.Configure<LojaDataSettings>(builder.Configuration.GetSection("LojaStoreDatabase"));
builder.Services.Configure<PedidoDataSettings>(builder.Configuration.GetSection("PedidoStoreDatabase"));

//Services Referente aos Models
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ProdutoService>();
builder.Services.AddSingleton<LojaService>();
builder.Services.AddSingleton<PedidoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => TokenService.ConfigureSwagguerJwtAthentication(c));

TokenService.ConfigureJWT(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
