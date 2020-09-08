using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.Connector.Middleware
{
    public class MakeMagicMiddleware
    {
        public MakeMagicMiddleware(RequestDelegate next)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        RequestDelegate Next { get; }

        public async Task InvokeAsync(HttpContext context)
        {
            await Next.Invoke(context);
        }
    }
}
