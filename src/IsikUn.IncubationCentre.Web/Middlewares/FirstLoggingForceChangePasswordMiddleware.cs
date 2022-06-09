using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Users;

namespace IsikUn.IncubationCentre.Web.Middlewares
{
    public class FirstLoggingForceChangePasswordMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IIdentityUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;
        private static Dictionary<string, bool> allowedUsers = new Dictionary<string, bool>();

        public FirstLoggingForceChangePasswordMiddleware(
            RequestDelegate next,
            IIdentityUserRepository userRepository,
            ICurrentUser currentUser)
        {
            this._next = next;
            this._userRepository = userRepository;
            this._currentUser = currentUser;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                if (httpContext.User.Identity.IsAuthenticated)
                {
                    var isAdmin = httpContext.User.IsInRole("admin");
                    if (!(isAdmin || isPathAllowed(httpContext.Request.Path) || isUserAllowed()))
                    {
                        var id = _currentUser.Id;
                        if (id.HasValue)
                        {
                            var currentUser = await _userRepository.GetAsync(id.Value);
                            var lastPasswordChangeTime = currentUser.ExtraProperties.GetValueOrDefault("LastPasswordChangeTime");
                            if (lastPasswordChangeTime == null)
                            {
                                httpContext.Response.Redirect("/Account/Manage?redirectType=ForceChangePassword");
                            }
                            else
                            {
                                allowedUsers.Add(
                                    string.Format(
                                        "{0}:{1}",
                                        _currentUser.TenantId.HasValue ? _currentUser.TenantId.Value : "null",
                                        _currentUser.UserName),
                                    true);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            await _next(httpContext);
        }

        private bool isUserAllowed()
        {
            bool isAllowed = false;
            allowedUsers.TryGetValue(
                string.Format(
                    "{0}:{1}",
                    _currentUser.TenantId.HasValue ? _currentUser.TenantId.Value : "null",
                    _currentUser.UserName),
                out isAllowed);
            return isAllowed;
        }

        private bool isPathAllowed(string Path)
        {
            return Path.Contains("/Account/Manage") || Path.EndsWith(".js") || Path.EndsWith(".js.map") || Path.StartsWith("/Abp/") || Path.StartsWith("/api/app/");
        }

    }

    public static class FirstLoggingForceChangePasswordMiddlewareExtensions
    {
        public static IApplicationBuilder UseFirstLoggingForceChangePasswordMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FirstLoggingForceChangePasswordMiddleware>();
        }
    }
}
