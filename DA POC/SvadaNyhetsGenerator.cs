using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DA_POC.Models.Pages;
using EPiServer;
using EPiServer.BaseLibrary.Scheduling;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.PlugIn;
using EPiServer.ServiceLocation;
using Newtonsoft.Json;

namespace DA_POC
{
    [ScheduledPlugIn(DisplayName = "Svadanyhetsgenerator")]
    public class SvadaNyhetsGenerator : JobBase
    {
        public override string Execute()
        {
            //CreateNews();
            //CreatePersons();
            AddPersonToNews();
            return "success";
        }

        private void AddPersonToNews()
        {
            var employees = GetEmployees().Select(e => e.FirstName + " " + e.LastName).ToArray();
            var random = new Random();

            var repo = ServiceLocator.Current.GetInstance<IContentRepository>();
            var pagereference = new PageReference(5);

            var news = repo.GetChildren<Nyhet>(pagereference);
            
            foreach (var n in news)
            {
                if (!string.IsNullOrEmpty(n.Author))
                {
                    continue;
                }
                var clone = (Nyhet) n.CreateWritableClone();

                var empNo = random.Next(employees.Length);
                var emp = employees[empNo];

                clone.Author = emp;
                clone.MainIntro = n.MainIntro + " Oppdiktet av " + emp + ".";

                repo.Save(clone, SaveAction.Publish);
            }
        }

        private void CreatePersons()
        {
            var employees = GetEmployees();

            var random = new Random();

            foreach (var e in employees)
            {
                var pagereference = new PageReference(1088);

                var repo = ServiceLocator.Current.GetInstance<IContentRepository>();

                var newPage = repo.GetDefault<Person>(pagereference);
                newPage.PageName = e.FirstName + " " + e.LastName;
                newPage.Department = e.Department;
                newPage.Email = e.Email;
                newPage.FirstName = e.FirstName;
                newPage.Gender = e.Gender;
                newPage.ImageUrl = e.ImageUrl;
                newPage.InterestGroup = e.InterestGroup;
                newPage.LastName = e.LastName;

                var nofCategories = random.Next(1, 5);
                var categories = new HashSet<int>();
                for (var i = 0; i < nofCategories; i++)
                {
                    var category = random.Next(1, 9);
                    categories.Add(category);
                }
                foreach (var c in categories)
                {
                    newPage.Category.Add(c);
                }

                repo.Save(newPage, SaveAction.Publish);
            }
        }

        private static List<Employee> GetEmployees()
        {
            List<Employee> employees;

            using (var reader = new StreamReader(@"c:\temp\ansatte\ansattliste.json"))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings {});
                employees = serializer.Deserialize<List<Employee>>(jsonReader);
            }
            return employees;
        }


        private static void CreateNews()
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
        }

        public class EmployeeList
        {
            public List<Employee> Employees { get; set; }
        }

        public class Employee
        {
            public string Department { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string Gender { get; set; }
            public string ImageUrl { get; set; }
            public string InterestGroup { get; set; }
            public string LastName { get; set; }
        }
    }
}