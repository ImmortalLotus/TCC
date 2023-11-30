using GLPIBot.GLPI.Domain.Models;

namespace GLPIBot.Api.Web.Extensions
{

    public static class InjecaoDeDependencia
    {
        public static WebApplicationBuilder AddInjetorDeDependencias(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return builder;
        }
    }
}

