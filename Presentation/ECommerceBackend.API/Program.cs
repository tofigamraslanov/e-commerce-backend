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
using ECommerceBackend.API.Configurations.ColumnWriters;
using ECommerceBackend.API.Extensions;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using Serilog.Context;
using Serilog.Sinks.PostgreSQL;

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

    var logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("Logs/Log.txt")
        .WriteTo.PostgreSQL(
            builder.Configuration.GetConnectionString("PostgreSQL"),
            "Logs",
            needAutoCreateTable: true,
            columnOptions: new Dictionary<string, ColumnWriterBase>()
            {
                { "message", new RenderedMessageColumnWriter() },
                { "message_template", new MessageTemplateColumnWriter() },
                { "level", new LevelColumnWriter() },
                { "timestamp", new TimestampColumnWriter() },
                { "exception", new ExceptionColumnWriter() },
                { "log_event", new LogEventSerializedColumnWriter() },
                { "user_name", new UserNameColumnWriter() },
            })
        .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
        .Enrich.FromLogContext()
        .CreateLogger();
    builder.Host.UseSerilog(logger);

    builder.Services.AddHttpLogging(logging =>
    {
        logging.LoggingFields = HttpLoggingFields.All;
        logging.RequestHeaders.Add("sec-ch-ua");
        logging.MediaTypeOptions.AddText("application/javascript");
        logging.RequestBodyLogLimit = 4096;
        logging.ResponseBodyLogLimit = 4096;
    });

    builder.Services
        .AddControllers(options => options.Filters.Add<ValidationFilter>())
        .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
        .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var tokenOptions = builder
        .Configuration
        .GetSection(TokenOptions.SectionName)
        .Get<TokenOptions>();

    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer("Admin", options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidAudience = tokenOptions.Audience,
                ValidIssuer = tokenOptions.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
                LifetimeValidator = (before, expires, token, parameters) => expires != null && expires > DateTime.UtcNow
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

    app.UseGlobalExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

    app.UseStaticFiles();

    app.UseSerilogRequestLogging();

    app.UseHttpLogging();

    app.UseCors();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.Use(async (context, next) =>
    {
        var userName = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
        LogContext.PushProperty("user_name", userName);
        await next();
    });

    app.MapControllers();

    app.Run();
}