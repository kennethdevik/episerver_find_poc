using System.IO;
using DA_POC.Models.Pages;
using EPiServer;
using EPiServer.BaseLibrary.Scheduling;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.PlugIn;
using EPiServer.ServiceLocation;

namespace DA_POC
{
    [ScheduledPlugIn(DisplayName = "Svadanyhetsgenerator")]
    public class SvadaNyhetsGenerator : JobBase
    {
        public override string Execute()
        {
            var dir = new DirectoryInfo(@"c:\temp\news");
            
            foreach (var fil in dir.GetFiles())
            {
                using (var stream = fil.OpenRead())
                using (var reader = new StreamReader(stream))
                {
                    var title = reader.ReadLine();
                    var ingress = reader.ReadLine();
                    var content = reader.ReadLine();

                    var pagereference = new PageReference(5);

                    var repo = ServiceLocator.Current.GetInstance<IContentRepository>();

                    var newPage = repo.GetDefault<Nyhet>(pagereference);
                    newPage.PageName = title;
                    newPage.MainIntro = ingress;
                    newPage.MainBody = new XhtmlString(content);
                    repo.Save(newPage, SaveAction.Publish);
                }
            }
            return "success";
        }
    }
}