using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrcHestrador.ApiClient;
using OrcHestrador.Infra.Context;
using OrcHestrador.UserStories.Implementations;
using OrcHestrador.UserStories.Interfaces;
using OrcHestrador.UserStories.Options;
using OrcHestrador.WorkerService;
using Refit;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

var host = Host.CreateDefaultBuilder(args)
.ConfigureServices((hostContext, services) =>
        {
            IConfiguration configuration = hostContext.Configuration;

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRefitClient<IGLPIClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration["URL_GLPI"]!); 
                c.DefaultRequestHeaders.Add("Origin", configuration["Url"]);
                c.DefaultRequestHeaders.Add("Authorization", configuration["Glpi_key"]);
            });


            services.Configure<GroupOptions>(
                configuration.GetSection(GroupOptions.NomeNoAmbiente)
                );
            services.Configure<MensagemOptions>(
                configuration.GetSection("Mensagens")
                );
            services.Configure<RequestOptions>(
                configuration.GetSection("RequestOptions")
                );
            var config = new MSSqlServerSinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true
            };
            Log.Logger = new LoggerConfiguration().MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning).WriteTo.MSSqlServer(configuration.GetConnectionString("DefaultConnection"), config, restrictedToMinimumLevel: LogEventLevel.Information).CreateLogger();
            //Log.Logger = new LoggerConfiguration().MinimumLevel.Override("Microsoft", LogEventLevel.Warning).WriteTo.MSSqlServer(configuration.GetConnectionString("DefaultConnection"), config, restrictedToMinimumLevel: LogEventLevel.Information).CreateLogger();

            services.AddDbContext<OrcHestradorContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IWorkerEntryPoint, WorkerEntryPoint>();
            services.AddScoped<ISolucionarChamado, SolucionarChamado>();
            services.AddScoped<IBuscarChamados, BuscarChamados>();
            services.AddScoped<ITriagemDeChamados, TriagemDeChamados>();
            services.AddScoped<IAtribuirChamados, AtribuirChamados>();
            services.AddScoped<ILimparChamados, LimparChamados>();
            services.AddScoped<IAvaliarChamados, AvaliarChamados>();
            services.AddScoped<IEnviarChamados, EnviarChamados>();
            services.AddScoped<IDesatribuirChamados, DesatribuirChamados>();
            services.AddScoped<IExecutarChamado, ExecutarChamado>();
            services.AddScoped<IValidarChamados, ValidarChamados>();
            services.AddHostedService<Worker>();
        }
    ).Build();

await host.RunAsync();