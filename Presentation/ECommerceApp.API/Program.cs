using ECommerceApp.Application.Validators.Products;
using ECommerceApp.Infrastructure.Filters;
using ECommerceApp.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceService();

builder.Services
    .AddCors(options =>
        options.AddDefaultPolicy(policy =>
            policy.WithOrigins("https://localhost:4200", "http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()));

builder.Services
    .AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
