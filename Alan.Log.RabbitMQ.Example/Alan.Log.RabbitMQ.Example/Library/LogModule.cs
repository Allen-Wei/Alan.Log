using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alan.Log.Core;
using Alan.Log.LogContainerImplement;


namespace Alan.Log.RabbitMQ.Example.Library
{
    public class LogModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.Error += Context_Error;
            context.BeginRequest += Context_BeginRequest;
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            var app = sender as HttpApplication;
            var req = app.Request;
            Alan.Log.LogContainerImplement.LogUtils.Current.LogDebug(id: Guid.NewGuid().ToString(), date: DateTime.Now, category: "request", message: String.Format("{0} {1}", req.HttpMethod, req.RawUrl));
        }

        private void Context_Error(object sender, EventArgs e)
        {
            var app = sender as HttpApplication;
            var ex = app.Server.GetLastError();

            Alan.Log.LogContainerImplement.LogUtils.Current.Log(
                id: Guid.NewGuid().ToString(),
                date: DateTime.Now,
                level: "error",
                category: "application",
                message: ex.Message,
                note: ex.StackTrace,
                position: ex.Source);
        }
    }
}