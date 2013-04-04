using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DA_POC.Models.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Api.Facets;
using EPiServer.Web.Mvc;
using EPiServer.Find.Cms;

namespace DA_POC.Controllers
{
    public class SearchPageController : PageController<SearchPage>
    {
        public ActionResult Index(SearchPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */


            return View(currentPage);
        }

        public ActionResult Prefix(string term)
        {
            var client = Client.CreateFromConfig();
            var result = client
                .Search<SearchablePage>()
                .Filter(x => x.Name.PrefixCaseInsensitive(term))
                .Select(x => x.Name)
                .StaticallyCacheFor(TimeSpan.FromHours(1))
                .GetResult();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Quick(string query, int page = 1)
        {
            var pageSize = 5;

            var client = Client.CreateFromConfig();
            var pages = client
                .Search<SearchablePage>()
                .For(query)
                .InFields(n => n.PageName, n => n.MainIntro)
                .ExcludeDeleted()
                .TermsFacetFor(data => data.PageTypeName)
                .TermsFacetFor(data => data.SearchCategories())
                //.Select(n => new {Title = n.PageName});
                //.Take(pageSize)
                //.Skip((page-1) * pageSize)
                .GetContentResult();

            var facets = new List<FacetResult>();

            var categoryFacet = (TermsFacet)pages.Facets["PageTypeName"];
            var categoryLinks = new FacetResult
                {
                    Name = "PageTypes",
                    Links = categoryFacet.Terms.Select(x => new FacetLink
                    {
                        Text = x.Term,
                        Count = x.Count
                    })
                };
            facets.Add(categoryLinks);


            var typeFacet = (TermsFacet)pages.Facets["SearchCategories"];
            var typeLinks = new FacetResult
            {
                Name = "Categories",
                Links = typeFacet.Terms.Select(x => new FacetLink
                {
                    Text = x.Term,
                    Count = x.Count
                })
            };
            facets.Add(typeLinks);

            var repository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();

            var compositeResult = new
                {
                    Hits = pages.SearchResult.Select(r => GetPage(r.ContentLink, repository)),
                    Facets = facets,
                };

            return Json(compositeResult, JsonRequestBehavior.AllowGet);
        }

        private Hit GetPage(ContentReference reference, IContentLoader repository)
        {
            var pagedata = repository.Get<SearchablePage>(reference);

            return new Hit
                       {
                           Title = pagedata.PageName,
                           Type = pagedata.PageTypeName,
                           MainIntro = pagedata.MainIntro,
                           Content = pagedata.MainBody != null ? pagedata.MainBody.ToHtmlString().Substring(0, 200) : string.Empty,
                           ImageUrl =  pagedata.ImageUrl,
                           Categories = pagedata.SearchCategories().Aggregate("", (current, c) => current + (", " + c))
                       };
        }
    }

    public class Hit
    {
        public string Title { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }

        public string MainIntro { get; set; }

        public string ImageUrl { get; set; }

        public string Categories { get; set; }
    }

    public class FacetResult
    {
        public string Name { get; set; }

        public IEnumerable<FacetLink> Links { get; set; }
    }

    public class FacetLink
    {
        public string Text { get; set; }

        public int Count { get; set; }
    }
}