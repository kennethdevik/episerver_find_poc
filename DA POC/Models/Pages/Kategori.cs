using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace DA_POC.Models.Pages
{
    [ContentType(DisplayName = "Kategori", GUID = "4134b786-b3d4-408c-9229-d7a4d8eef2d2", Description = "")]
    public class Kategori : PageData
    {
        
                [CultureSpecific]
                [Editable(true)]
                [Display(
                    Name = "Main body",
                    Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual XhtmlString MainBody { get; set; }
         
        [CultureSpecific]
        [Editable(true)]
        public virtual string Navn { get; set; }
    }
}