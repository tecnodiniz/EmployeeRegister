using System.Data.Entity;
using EmployeeRegister.Model;

namespace EmployeeRegister.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=MyDbContext") 
        {
            Database.SetInitializer <MyDbContext>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
