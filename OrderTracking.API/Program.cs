using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using OrderTracking.API.Controllers;
using OrderTracking.API.Middlewares;
using OrderTracking.Business;
using OrderTracking.Business.Mapping;
using OrderTracking.Business.Services.Abstarct;
using OrderTracking.Business.Services.Concreate;
using OrderTracking.Business.Validation;
using OrderTracking.Core.Interfaces;
using OrderTracking.DataAccess.Context;
using OrderTracking.DataAccess.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);
// 1. Database Baðlantýsý
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. AutoMapper Kaydý
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 3. UnitOfWork Kaydý
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 4. Business Servisleri Kaydý
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllers();
builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


builder.Services.AddApplicationRegistration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
