using Services.RequestHandler.GetAvailableTicket;
using Services.Validator.GetAvailableTicket;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using Entity.Entity;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Host.UseSerilog((ctx, config) => config
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/WebApiTraining.log", LogEventLevel.Warning, rollingInterval: RollingInterval.Day)
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

builder.Services.AddMediatR(typeof(CreateAvailableTicketHandler));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateAvailableTicketValidator));

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

app.UseExceptionHandler("/error");

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting web api host.");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program
{

}