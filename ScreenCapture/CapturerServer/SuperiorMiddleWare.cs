using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace CapturerServer
{
    public class SuperiorMiddleWare : OwinMiddleware
    {
        public SuperiorMiddleWare(OwinMiddleware next) : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            var path = context.Request.Path.Value;
            if (path == "/")
            {
                context.Response.StatusCode = 302;
                context.Response.Redirect("https://www.google.com");
            }

            await this.Next.Invoke(context);
        }
    }
}
