using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ToDuoAPI.Configurations;
using ToDuoAPI.Contracts;
using ToDuoAPI.Data;
using ToDuoAPI.Models;
using ToDuoAPI.Repository;
using ToDuoAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ToDuoConnectionString");
builder.Services.AddDbContext<ToDuoDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<ToDuoUsers, IdentityRole<int>>()
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<ToDuoDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
             .AllowAnyOrigin()
             .AllowAnyMethod());
});

//This is for Seq
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAdventures, AdventuresRespository>();
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddScoped<ICities, CityRepository>();
builder.Services.AddScoped<IState, StateRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
