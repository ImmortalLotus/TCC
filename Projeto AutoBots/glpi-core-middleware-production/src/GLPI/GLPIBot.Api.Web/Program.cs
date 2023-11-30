using GLPIBot.GLPI.Domain.Models;
using GLPIBot.GLPI;
using GLPIBot.Api.Web.Extensions;
using CDevAuth.DependencyInjections;
using GLPIBot.Infra;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add client de api do glpi e singleton de sessão
builder.AddInjetorDeDependencias();
builder.Services.Configure<GlpiOptions>(
    x => x.UserId = builder.Configuration["user_id"]);

builder.Services.AddDbContext<GlpiContext>(options=> options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));



builder.Services.AddControllers();
builder.Services.AddCDev(options =>
{
    options.CDevBaseAddress = builder.Configuration["UrlCDev"];
    options.SendLogPayload = false; //O valor default é true, use false apenas se o payload for muito grande
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c=>c.EnableAnnotations());
builder.WebHost.UseOpenShiftIntegration();
var app = builder.Build();
// Configure the HTTP request pipeline.
//app.UseCDev(_context => !_context.Request.Path.StartsWithSegments("/swagger"));
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
 
app.Run();
