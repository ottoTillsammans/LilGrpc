using System;
using Microsoft.EntityFrameworkCore;

namespace AdvanticaServer
{
    public class Context : DbContext
    {
        private string Path { get; set; }
        public DbSet<Employee> Employees { get; set; } = null!;

        public Context()
        {
            Database.EnsureCreated();
        }
        public Context(string path)
        {
            this.Path = path;
            Database.EnsureCreated();
        }
    }
}
