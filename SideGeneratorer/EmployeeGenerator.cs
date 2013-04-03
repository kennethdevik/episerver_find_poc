using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DA_POC.Models.Pages;
using NUnit.Framework;
using Newtonsoft.Json;

namespace SideGeneratorer
{
    [TestFixture]
    public class EmployeeGenerator
    {
        [Test]
        public void Generate()
        {
            List<Employee> employees;

            using (var reader = new StreamReader(@"c:\temp\ansatte\ansattliste.json"))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings{});
                employees = serializer.Deserialize<List<Employee>>(jsonReader);
            }

            var persons = employees.Select(e => new Person
                                                    {
                                                        Department = e.Department,
                                                        Email = e.Email,
                                                        FirstName = e.FirstName,
                                                        Gender = e.Gender,
                                                        ImageUrl = e.ImageUrl,
                                                        InterestGroup = e.InterestGroup,
                                                        LastName = e.LastName
                                                    });

            foreach (var person in persons)
            {
                Console.Out.WriteLine(person.FirstName + " " + person.LastName);
            }
        }

        public class EmployeeList
        {
            public List<Employee> Employees { get; set; }
        }

        public class Employee
        {
            public virtual string Department { get; set; }
            public virtual string Email { get; set; }
            public virtual string FirstName { get; set; }
            public virtual string Gender { get; set; }
            public virtual string ImageUrl { get; set; }
            public virtual string InterestGroup { get; set; }
            public virtual string LastName { get; set; }
        }
    }
}