using MatchInfo.WebApi.DbContexts;
using MatchInfo.WebApi.Filters;
using MatchInfo.WebApi.GlobalErrorHandling.Extensions;
using MatchInfo.WebApi.Repositories;
using MatchInfo.WebApi.RepositoriesAbstractions;
using MatchInfo.WebApi.ServicesAbstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());
;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddDbContextPool<MatchInfoDbContext>(dbContextOptions =>
{
    dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:MatchInfoDBConnectionString"]);
});

//builder.Services.AddDbContext<MatchInfoDbContext>(dbContextOptions => 
//    dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:MatchInfoDBConnectionString"])
//);


builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.Configure<ApiBehaviorOptions>(opts => opts.SuppressModelStateInvalidFilter = true);
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
