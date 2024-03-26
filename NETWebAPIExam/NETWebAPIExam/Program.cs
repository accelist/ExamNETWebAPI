using Entity.Entity;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Services.RequestHandler.Ticket;
using Services.Validators;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog((ctx, config) => config
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/Log-20240326.log", LogEventLevel.Warning, rollingInterval: RollingInterval.Day)
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


builder.Services.AddMediatR(typeof(GetTicketHandler));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateBookedValidator));

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

//app.Run();

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

//public partial class Program { }
