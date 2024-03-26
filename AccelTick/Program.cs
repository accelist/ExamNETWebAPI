using Entity.Entity;
using Microsoft.EntityFrameworkCore;
using Services.RequestHandler.ManageTickets;
using MediatR;
using System.Reflection;
using Services.Validators;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContextPool<DBContext>(dbContextBuilder =>
{
    dbContextBuilder.UseSqlite(configuration.GetConnectionString("Sqlite"));
});

builder.Services.AddMediatR(typeof(GetTicketDataHandler));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateBookedTicketsValidator));

builder.Services.AddMediatR(typeof(CreateBookedTicketsValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(GetBookTicketValidator));

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

app.Run();
