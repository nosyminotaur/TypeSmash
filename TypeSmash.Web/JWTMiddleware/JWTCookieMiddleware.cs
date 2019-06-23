using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TypeSmash.Web.JWTMiddleware
{
    public class JWTCookieMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string JWT_COOKIE_NAME = "jwtcookie";
        private readonly string AUTH_HEADER = "Authorization";
        public JWTCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string jwt = context.Request.Cookies[JWT_COOKIE_NAME];
            if (jwt != null)
            {
                if (!context.Request.Headers.ContainsKey(AUTH_HEADER))
                {
                    context.Request.Headers.Append(AUTH_HEADER, "Bearer " + jwt);
                }
            }
            await _next.Invoke(context);

        }
    }
}
