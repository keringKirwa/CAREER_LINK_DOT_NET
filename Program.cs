using CareerLinkServer.Authentication;
using CareerLinkServer.DataBaseContext;
using CareerLinkServer.Filters;
using CareerLinkServer.OptionsSetUp;
using CareerLinkServer.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

var productServiceDescriptor =
    new ServiceDescriptor(typeof(IProductService), typeof(ProductService), ServiceLifetime.Singleton);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")), ServiceLifetime.Singleton);

builder.Services.TryAddEnumerable(productServiceDescriptor);

//SERVICE Layer 
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<JwtProvider>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

 // when somebody injects an instance of the IOptions<JwtOptions> class , the framework will  inject it from  the Ioc cntainer.
 
builder.Services.ConfigureOptions<JwtOptionsSetUp>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetUp>();
builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());


var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Database initialization options
if (app.Environment.IsDevelopment())
{
    builder.Services.BuildServiceProvider().GetService<AppDbContext>()?.Database.EnsureCreated();
}


app.Run();
