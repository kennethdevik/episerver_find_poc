using System;
using System.Web.Mvc;

namespace DA_POC
{
    public class Global : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}