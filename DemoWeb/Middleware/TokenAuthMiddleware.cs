using Shared.Helper;

namespace SimplePOSWeb.Middleware
{
    public class TokenAuthMiddleware
    {
        private readonly RequestDelegate _next;
        public TokenAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.Cookies.TryGetValue(ConstantHelper.Auth.UserWebTokenCookie, out string token);
            if (token != null)
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }

            await _next(context);
        }
    }
}
