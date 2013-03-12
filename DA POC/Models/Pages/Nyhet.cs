using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace DA_POC.Models.Pages
{
    [ContentType(DisplayName = "Nyhet", GUID = "853b70cd-e8b6-47dd-adf2-0d8c6ee64356", Description = "")]
    public class Nyhet : PageData
    {
        
                [CultureSpecific]
                [Editable(true)]
                [Display(
                    Name = "Main body",
                    Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual XhtmlString MainBody { get; set; }
         
    }
}