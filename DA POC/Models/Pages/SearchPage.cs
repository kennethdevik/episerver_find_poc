using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace DA_POC.Models.Pages
{
    [ContentType(DisplayName = "SearchPage", GUID = "2574e272-24b9-4e94-a483-84868194c04c", Description = "")]
    public class SearchPage : SearchablePage
    {
        
                [CultureSpecific]
                [Editable(true)]
                [Display(
                    Name = "Main body",
                    Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public override XhtmlString MainBody { get; set; }
         
    }
}