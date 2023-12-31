using API.Data;
using API.Data.SeedData;
using API.Extentions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();

builder.Services.AddDbContext<StoreContext> (options => 
        options.UseNpgsql(builder.Configuration.GetConnectionString ("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        
        await StoreContextSeed.SeedAsync(context, loggerFactory);
    } catch (Exception ex) {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError (ex, "An error occured during migrations");
    }
}

app.Run();
