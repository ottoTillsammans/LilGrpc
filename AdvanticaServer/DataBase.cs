using System;
using Microsoft.EntityFrameworkCore;

namespace AdvanticaServer
{
    public class Context : DbContext
    {
        private string server = string.Empty;
        private string dBName = string.Empty;
        private string credentials = string.Empty;
        private string connectionString = string.Empty;
        public DbSet<Employee> Employees { get; set; } = null!;

        public Context(string connectionString)
        {
            this.connectionString = connectionString;
            
            Database.EnsureCreated();
        }
        public Context(string server, string dBName, string credentials)
        {
            this.server = server;
            this.dBName = dBName;
            this.credentials = credentials;
            this.connectionString = string.Format("Server={0};Database={1};{2};", server, dBName, credentials);

            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
