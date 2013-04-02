using EPiServer.Core;

namespace DA_POC.Models.Pages
{
    public abstract class SearchablePage : PageData
    {
        public abstract string MainIntro { get; set; }
        public abstract XhtmlString MainBody { get; set; }
    }
}