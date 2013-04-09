using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using DA_POC.Models.Pages;
using EPiServer.Find.ClientConventions;
using EPiServer.Find.Cms;
using EPiServer.Find.Cms.Conventions;
using EPiServer.Find.Framework;
using EPiServer.Find.UnifiedSearch;
using EPiServer.Web.Hosting;

namespace DA_POC
{
    public static class ExtMethods
    {
        public static string SearchTypeName(this VersioningFile file, bool overriding)
        {
            return "File";
        }

        public static string SearchTypeName(this Nyhet pagedata, bool overriding)
        {
            return "Nyhet";
        }

        public static string SearchTypeName(this Person pagedata, bool overriding)
        {
            return "Person";
        }
    }
    public class Global : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            // Aktiverer indeksering av filer knyttet til sider
            ContentIndexer.Instance.Conventions.EnablePageFilesIndexing();
            // Aktiverer indeksering av globale filer
            FileIndexer.Instance.Conventions.ShouldIndexVPPConvention
  = new VisibleInFilemanagerVPPIndexingConvention();

            SearchClient.Instance.Conventions.ForInstancesOf<VersioningFile>()
                .ExcludeField(x => x.SearchTypeName()) // Exclude the default
                .IncludeField(x => x.SearchTypeName(true)); // Include our own

            SearchClient.Instance.Conventions.ForInstancesOf<Person>()
                .ExcludeField(x => x.SearchTypeName()) // Exclude the default
                .IncludeField(x => x.SearchTypeName(true)); // Include our own

            SearchClient.Instance.Conventions.ForInstancesOf<Nyhet>()
                .ExcludeField(x => x.SearchTypeName()) // Exclude the default
                .IncludeField(x => x.SearchTypeName(true)); // Include our own

            SearchClient.Instance.Conventions.UnifiedSearchRegistry.ForInstanceOf<Person>()
                        .ProjectImageUriFrom(x => new Uri(x.ImageUrl));
        }
    }
}