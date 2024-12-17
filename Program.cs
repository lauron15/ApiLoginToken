using ApiLoginToken.Data;
using ApiLoginToken.Interfaces;
using ApiLoginToken.Models;
using ApiLoginToken.Repository;
using ApiLoginToken.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<TokenService>();


var app = builder.Build();



app.MapPost("/login", (TokenService service) =>
{
    // Par�metros passados diretamente (poderiam ser enviados no corpo da requisi��o)
    var id = 1;
    var email = "teste@lauro.io";
    var senha = "123"; // Senha, se necess�rio, mas n�o usada no token
    var roles = new[] { "student", "premium" };

    // Criando o usu�rio com os dados passados
    var user = new User
    {
        Id = id,
        Email = email,
        Senha = senha,  // A senha n�o � utilizada no token, apenas para o banco
        Roles = roles
    };

    // Gerando o token usando o servi�o
    var token = service.Generate(user);

    // Retornando o token gerado
    return Results.Ok(new { Token = token });
});



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
