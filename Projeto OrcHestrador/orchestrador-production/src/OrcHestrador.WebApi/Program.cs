using Microsoft.EntityFrameworkCore;
using OrcHestrador.Infra.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());
builder.Services.AddDbContext<OrcHestradorContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.WebHost.UseOpenShiftIntegration();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();