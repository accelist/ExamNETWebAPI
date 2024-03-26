using Microsoft.EntityFrameworkCore;
using Entity;
using FluentValidation;
using MediatR;
using Services.RequestHandlers;
using Serilog;
using Serilog.Events;
using Services.RequestHandlers.Category;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog((ctx, config) => config
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/WebApiTraining.Log", LogEventLevel.Warning, rollingInterval: RollingInterval.Day)
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

builder.Services.AddMediatR(typeof(GetTicketDataListHandler));

builder.Services.AddMediatR(typeof(CreateCategoryDataHandler));

builder.Services.AddMediatR(typeof(GetCategoryDataListHandler));

builder.Services.AddMediatR(typeof(GetTicketDataListHandler));

builder.Services.AddMediatR(typeof(CreateBookedTicketHandler));

//builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateCustomerValidator));

//builder.Services.Configure<AppSettings>(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Dependency Injection
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DBContext>();
    db.Database.EnsureCreated();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

try
{
    Log.Information("Starting web api host");
    //throw new Exception("MELEDAK");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host Terminated");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
 