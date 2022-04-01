using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace AdvanticaServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string server = @"(localdb)\mssqllocaldb";
            string dBName = "advantica";
            string creds = "Trusted_Connection = True";
            DateTime date = new DateTime(1999, 2, 22);

            using (Context db = new Context(server, dBName, creds))
            {
                bool isDbAvail = db.Database.CanConnect();

                if (!isDbAvail)
                    return;

                Employee engineer = new Employee("Anna", "Plaschg", "", GenderType.Male);
                Employee programmer = new Employee("Kate", "Smith", "", GenderType.Male, date, false);
                
                db.Add(engineer); 
                db.Add(programmer);
                db.SaveChanges();

                var employees = db.Employees;

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.Id}, {e.Name}, {e.Surname}, {e.Gender}, {e.Birthdate}");
                }
                Console.WriteLine("Изменения успешно сохранены");
            }
        }
    }
}
