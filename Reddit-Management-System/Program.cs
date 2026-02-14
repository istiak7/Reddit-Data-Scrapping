using Microsoft.AspNetCore.ResponseCompression;
using Reddit_Management_System.Application;
using Reddit_Management_System.Application.Common.Utilities;
using Reddit_Management_System.Application.Features.Email.Command.Dtos;
using Reddit_Management_System.Data.Setups;
using Reddit_Management_System.DependencyExtensions;
using Reddit_Management_System.Mappers;
using Reddit_Management_System.Middleware;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.AddSwagger();
builder.Services.AddHttpClient();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddAuthPolicies();
builder.AddJWTAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.EnableForHttps = true;
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings"));

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowFrontend", builder =>
//     {
//         builder
//             .WithOrigins("http://localhost:4200") // Angular app URL
//             .AllowAnyHeader()
//             .AllowAnyMethod();
//     });
// });

#region BackgroundJob Configuration

builder.AddBackgroundJobs();

#endregion


#region Redis Cache Configuration

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("Redis:ConnectionString");
    options.InstanceName = builder.Configuration.GetValue<string>("Redis:InstanceName");
});

#endregion

#region Initialize CommonMethods

CommonMethods.Initialize(builder.Configuration);
#endregion

#endregion

#region Dependency Injection For Entity Framework Core Implementation (Infustructure)

string? connectionString = builder.Configuration.GetValue<string>("DbSettings:DbConnectionString");
builder.Services.AddPersistence(connectionString);

#endregion
builder.Services.AddApplication();

#region AutoMapper Configuration

builder.Services.AddAutoMapper(
    typeof(DefaultProfile),
    typeof(RequestMapper),
    typeof(ResponseMapper));

#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseResponseCompression();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomMiddleware();
app.MapControllers();
app.Run();

#endregion