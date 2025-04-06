using ControlePlus_BackEnd.db;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DbConnectionString");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));


builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


var app = builder.Build();

// app.UseHttpsRedirection(); não estou utilizando o HTTPS agora por motivos de segurança

app.UseAuthorization();

app.MapControllers();

app.Run();
