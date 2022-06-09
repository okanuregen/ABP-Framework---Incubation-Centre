using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace IsikUn.IncubationCentre.Web.Middlewares
{
    public class DashboardRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public DashboardRedirectMiddleware(
            RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var url = httpContext.Request.Path;
            if(url.HasValue && url.Value == "/")
            {
                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    httpContext.Response.Redirect("/Account/Login");
                    return;
                }
                else
                {
                    if (httpContext.User.IsInRole("Mentor"))
                    {
                        httpContext.Response.Redirect("/Mentors/Dashboard");
                        return;
                    }
                    if (httpContext.User.IsInRole("Entrepreneur") || httpContext.User.IsInRole("Girişimci"))
                    {
                        httpContext.Response.Redirect("/Entrepreneurs/Dashboard");
                        return;
                    }
                    if (httpContext.User.IsInRole("Investor") || httpContext.User.IsInRole("Yatırımcı"))
                    {
                        httpContext.Response.Redirect("/Investors/Dashboard");
                        return;
                    }
                    if (httpContext.User.IsInRole("System Manager") || httpContext.User.IsInRole("SystemManager") || httpContext.User.IsInRole("SistemYöneticisi"))
                    {
                        httpContext.Response.Redirect("/SystemManagers/Dashboard");
                        return;
                    }
                    if (httpContext.User.IsInRole("Collaborator") || httpContext.User.IsInRole("İşOrtağı"))
                    {
                        httpContext.Response.Redirect("/Collaborators/Dashboard");
                        return;
                    }
                }
            }
            await _next(httpContext);
        }

    }

    public static class DashboardRedirectMiddlewareExtensions
    {
        public static IApplicationBuilder DashboardRedirectMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DashboardRedirectMiddleware>();
        }
    }
}
