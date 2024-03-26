using Entity.Entity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using FluentValidation;

using Serilog;
using MediatR;
using Serilog.Events;
using Services.RequestHandlers.ManageCategories;
using Services.Validator.Categories;
using Services.RequestHandlers.ManageBooked;
using Services.Validator.BookTicket;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog((ctx, config) => config
       .MinimumLevel.Information()
       .WriteTo.Console()
       .WriteTo.File($"Log-{DateAndTime.Today}.txt", LogEventLevel.Warning, rollingInterval: RollingInterval.Day)
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<DBContext>(dbContextBuilder =>
{
    dbContextBuilder.UseSqlite(configuration.GetConnectionString("Sqlite"));
});

builder.Services.AddMediatR(typeof(CreateCategoryHandler));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateCategoryDataValidator));
builder.Services.AddMediatR(typeof(CreatedBookedHandler));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateBookTicketValidator));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DBContext>(); 
    db.Database.EnsureCreated();
}

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting WEb Api Host . . .");
    app.Run();
    return 0;

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host Terminated Unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}