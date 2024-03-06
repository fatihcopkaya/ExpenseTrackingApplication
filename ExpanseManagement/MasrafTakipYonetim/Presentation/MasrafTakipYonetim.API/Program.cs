using MasrafTakipYonetim.Persistence;
using MasrafTakipYonetim.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;
using MasrafTakipYonetim.API;
using Hangfire;
using Hangfire.SqlServer;
using MasrafTakipYonetim.Application.Services;
using MassTransit;

using MediatR;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MasrafTakipYonetim.Application.Helpers;
using MasrafTakipYonetim.Infrastructure.Helpers;
using Serilog;
using Serilog.Core;
using System.Security.Claims;
using Serilog.Context;
using MasrafTakipYonetim.API.Configuration;
using StackExchange.Redis;
using MasrafTakipYonetim.Infrastructure.Redis;
using MasrafTakipYonetim.Infrastructure.Attributes;
using FluentValidation.AspNetCore;
using static System.Net.Mime.MediaTypeNames;
using RabbitMQ_PublisherQueue;
using Shared;
using MasrafTakipYonetim.Application.CrossCutting;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddControllers(option => option.Filters.Add<Validation_Filter>())
    .AddFluentValidation(configuration =>
    {
        configuration
            .RegisterValidatorsFromAssemblyContaining<MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses.CreateAppUserCommandValidator>();
        configuration
            .RegisterValidatorsFromAssemblyContaining<MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseRequestsAndResponses.CreateExpenseCommandValidator>();
        configuration
           .RegisterValidatorsFromAssemblyContaining<MasrafTakipYonetim.Application.Cqrs.Commands.PaymentRequestsAndResponses.CreatePaymentCommandValidator>();

    })
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);



builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddScoped<IMailHelper, MailHelper>();
var sqlScriptConfiguration = builder.Configuration;
MigrationConfiguration.SqlsPath = sqlScriptConfiguration["Sqls"];






builder.Services.AddCors(options =>
{

    // CORS politikaları yapılandırıldı
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.WithOrigins("*")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.MongoDB(
        databaseUrl: _configuration["ConnectionStrings:MongoDb"],
        collectionName: "Logs",
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
    )
    .Enrich.FromLogContext()
    .Enrich.With<CustomUserNameColumnWriter>()
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(log);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var deneme = _configuration["JWT:ValidAudience"];

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.ConfigurationOptions = new ConfigurationOptions
    {
        EndPoints = { "127.0.0.1:1453" }, // Redis sunucu adresi ve portu
       
        // Diğer ayarlar...
        SyncTimeout = 5000, // Zaman aşımı (timeout) değeri
        AbortOnConnectFail = false,
    };
});

//builder.Services ConfigureServices(IServiceCollection services)
//{
//    // Diğer servis konfigürasyonları burada

//    services.AddControllers()
//        .AddJsonOptions(options =>
//        {
//            options.JsonSerializerOptions.Converters.Add(new GuidToStringConverter());
//        });
//}
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateAudience = true,
    ValidateIssuer = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidAudience = _configuration["JWT:ValidAudience"],
    ValidIssuer = _configuration["JWT:ValidIssuer"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding
                   .UTF8.GetBytes(_configuration["JWT:Secret"])),
    ClockSkew = TimeSpan.Zero,
    NameClaimType = ClaimTypes.Name
};

builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = tokenValidationParameters;

});
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MasrafTakipYonetim.API"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Bearer authorization token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer", // must be lowercase!!!
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
    });
});
JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Newtonsoft.Json.Formatting.Indented,
    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
};
var mssqlConnectionString = builder.Configuration.GetConnectionString("MSSQL");

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(
        mssqlConnectionString, // Bağlantı dizesini buradan alıyoruz
        new SqlServerStorageOptions
        {
            QueuePollInterval = TimeSpan.FromSeconds(10),
            JobExpirationCheckInterval = TimeSpan.FromHours(1),
            CountersAggregateInterval = TimeSpan.FromMinutes(5),
            PrepareSchemaIfNecessary = true,
            DashboardJobListLimit = 25000,
            TransactionTimeout = TimeSpan.FromMinutes(1)

        }
    )
);
builder.Services.AddHangfireServer();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
builder.Services.AddSingleton<ICacheService, RedisService>();
builder.Services.AddScoped<CaptchaHelper>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
//app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();
// CORS politikaları etkinleştirildi
app.UseStaticFiles();
app.UseCors("AllowOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireServer();
app.UseHangfireDashboard("/hangfire");

var createPaymentsByUserCron = _configuration.GetSection("HangfireCrons")["CreatePaymentForUsers"];
var createPaymentsofMounthByUserCron = _configuration.GetSection("HangfireCrons")["CreatePaymentofMounth"];

RecurringJob.AddOrUpdate<IPaymentService>("Create Payment For Users", x => x.CreatePaymentsJob(), createPaymentsByUserCron);
RecurringJob.AddOrUpdate<IPaymentService>("Create Payment of Mounth", x => x.CreatePaymentsofMountJob(), createPaymentsofMounthByUserCron);



app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated == true ? context.User.Identity.Name : "Anonymous";
    Serilog.Context.LogContext.PushProperty("UserName", username);
    await next();
});

app.MapControllers();

app.Run();
