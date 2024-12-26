using IfoodParaguai.Models;
using IfoodParaguai.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<UserDataSettings>(builder.Configuration.GetSection("UserStoreDatabase"));
builder.Services.AddControllers();
builder.Services.AddSingleton<UserService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
