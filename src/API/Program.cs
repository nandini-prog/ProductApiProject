using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Application.Validators;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using API.Middlewares;


var builder = WebApplication.CreateBuilder(args);


// Add controllers (we’re using attribute-based controllers)
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        // Register all validators in the Application layer
        fv.RegisterValidatorsFromAssemblyContaining<CreateProductDtoValidator>();
    });

// Add DbContext (EF Core) with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IProductService, ProductService>();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// global exceptional handling here 
app.UseGlobalExceptionHandling();

app.UseHttpsRedirection();

app.Run();