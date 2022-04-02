using System;

namespace AdvanticaGrpcServer
{
    public enum GenderType
    {
        Male,
        Female
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronym { get; set; }
        public DateTime Birthdate { get; set; }
        public GenderType Gender { get; set; }
        public bool HasChildren { get; set; }

        public Employee(string name, string surname, string patronym, GenderType gender)
        {
            this.Name = name;
            this.Surname = surname;
            this.Patronym = patronym;
            this.Gender = gender;
        }

        public Employee(
            string name, string surname, string patronym, 
            GenderType gender, DateTime date, bool hasChildren)
        {
            this.Name = name;
            this.Surname = surname;
            this.Patronym = patronym;
            this.Gender = gender;
            this.Birthdate = date;
            this.HasChildren = hasChildren;
        }
    }
}
