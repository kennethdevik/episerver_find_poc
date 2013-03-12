using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using EPiServer.Core;
    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Mvc;

namespace DA_POC
{


        [ModuleDependency(typeof(ServiceContainerInitialization))]
        [InitializableModule]
        public class MvcTemplatesInitializer : IInitializableModule
        {

            public void Initialize(InitializationEngine context)
            {
                RegisterRoutes(RouteTable.Routes);
            }

            public void Preload(string[] parameters) { }

            public void Uninitialize(InitializationEngine context) { }

            private void RegisterRoutes(RouteCollection routeCollection)
            {
                routeCollection.MapRoute("quicksearch", "quicksearch/q/{query}", new
                {
                    controller = "SearchPage",
                    action = "Quick",
                    query = "*"
                });
                routeCollection.MapRoute("autocomplete", "autocomplete/q/{query}", new
                {
                    controller = "SearchPage",
                    action = "Prefix",
                    query = "*"
                });

            }

           
        }
    }