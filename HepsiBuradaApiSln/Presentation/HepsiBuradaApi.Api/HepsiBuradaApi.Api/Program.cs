using HepsiBuradaApi.Application;
using HepsiBuradaApi.Application.Exceptions;
using HepsiBuradaApi.Mapper;
using HepsiBuradaApi.Persistance;
using HepsiBuradaApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var env = builder.Environment;

builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddApplicaiton();
builder.Services.AddCustomMapper();
builder.Services.AddInfastructure(builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandlingMidddleWare();

app.UseAuthorization();

app.MapControllers();

app.Run();
