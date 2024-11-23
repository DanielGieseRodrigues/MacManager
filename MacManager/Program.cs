using MacManager.Domain.ValueObjects;
using MacManager.Infra;
using MacManager.Infra.Data;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Aqui eu fiz uma magiazinha para representar mais legal o enumerador no swagger, fazendo com que exiba os valores caracteres, para voces terem uma experiencia mais bacana :)
builder.Services.AddSwaggerGen(c =>
{
    c.MapType<AreaCozinha>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = Enum.GetNames(typeof(AreaCozinha)).Select(e => new OpenApiString(e)).Cast<IOpenApiAny>().ToList()
    });
    c.MapType<StatusPedido>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = Enum.GetNames(typeof(StatusPedido)).Select(e => new OpenApiString(e)).Cast<IOpenApiAny>().ToList()
    });
});

//Garantindo o cors 1
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

//Chama as injecoes de dependencias de use cases e repositorios.
builder.Services.AdicionarServiceCollection();

var app = builder.Build();

//Garantindo o cors 2
app.UseCors("AllowAll");

//Precisa botar isso aqui para garantir o seed do EF, isso inclusive esta numa thread sendo discutida para ver se no futuro vai precisar ou nao fazer isso, e o meu papel eu fiz ( Fui lá encher o saco pedindo pra tirar a obrigação xD ).
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MacManagerContext>();
    context.Database.EnsureCreated();
}

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
