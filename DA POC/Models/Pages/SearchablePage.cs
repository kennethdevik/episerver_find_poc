using EPiServer.Core;

namespace DA_POC.Models.Pages
{
    public abstract class SearchablePage : PageData
    {
        public abstract XhtmlString MainBody { get; set; }
    }
}