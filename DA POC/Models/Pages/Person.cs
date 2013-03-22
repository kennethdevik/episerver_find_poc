﻿using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace DA_POC.Models.Pages
{
    [ContentType(DisplayName = "Person", GUID = "579e6d8c-4a89-4940-b95a-66721b8eebaa", Description = "")]
    public class Person : SearchablePage
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