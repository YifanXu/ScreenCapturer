using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using System.IO;

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
                context.Response.Redirect("/static/default.html");
                return;
            }
            if(path == "/broadcast_img")
            {
                if (DataStatic.jpegDisplay == null) return;
                context.Response.ContentType = "image/jpeg";
                context.Response.Write(DataStatic.jpegDisplay, 0, DataStatic.jpegDisplay.Length);
                return;
            }
            if(path == "/imgInfo")
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(DataStatic.imgID.ToString());
                return;
            }

            await this.Next.Invoke(context);
        }
    }
}
