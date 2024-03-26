using Microsoft.EntityFrameworkCore;
using MediatR;
using Serilog;
using FluentValidation;
using Serilog.Events;
using Entity.Entity;
using Services.ResponseHandlers.Ticket;
using Services.Validator.Ticket;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog((ctx, config) => config
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-", LogEventLevel.Warning, rollingInterval : RollingInterval.Day)
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<DBContext>(dbContextBuilder =>
{
    dbContextBuilder.UseSqlite(configuration.GetConnectionString("Sqlite"));;
});

builder.Services.AddMediatR(typeof(CreateTicketHandler));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateTicketValidator));

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
    Log.Information("Starting Web API host");
    app.Run();
    return 0;
}
catch(Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpedtedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}