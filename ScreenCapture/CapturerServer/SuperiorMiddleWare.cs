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
                var MF = MainForm.staticInstance;
                if (MF.jpegDisplay == null) return;
                context.Response.ContentType = "image/jpeg";
                context.Response.Write(MF.jpegDisplay, 0, MF.jpegDisplay.Length);
                return;
            }
            if(path == "/imgInfo")
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(MainForm.staticInstance.imgID.ToString());
                return;
            }

            await this.Next.Invoke(context);
        }
    }
}
