using System;

namespace AdvanticaServer
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

        public Employee(
            int id, string name, string surname, string patronym,
            DateTime date, GenderType gender, bool hasChildren)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Patronym = patronym;
            this.Birthdate = date;
            this.Gender = gender;
            this.HasChildren = hasChildren;
        }
    }
}
