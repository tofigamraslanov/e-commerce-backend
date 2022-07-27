using ECommerceBackend.Application;
using ECommerceBackend.Application.Options.Token;
using ECommerceBackend.Application.Validators.Products;
using ECommerceBackend.Infrastructure;
using ECommerceBackend.Infrastructure.Filters;
using ECommerceBackend.Infrastructure.Services.Storage.Azure;
using ECommerceBackend.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{

    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddInfrastructureServices();
    builder.Services.AddPersistenceServices();

    // builder.Services.AddStorage(StorageType.Local);
    // builder.Services.AddStorage<LocalStorage>();
    builder.Services.AddStorage<AzureStorage>();

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

    var tokenOptions = builder
        .Configuration
        .GetSection(TokenOptions.Token)
        .Get<TokenOptions>();

    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer("Admin",options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidAudience = tokenOptions.Audience,
                ValidIssuer = tokenOptions.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
            };
        });
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles();

    app.UseCors();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
