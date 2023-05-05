using DateBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBase.Service
{
    public class StoreUserContext : DbContext
    {
        public StoreUserContext()
        {
            //Database.EnsureCreated();            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = UserBase; Integrated Security = True; Connect Timeout = 30;");
        }
        protected internal virtual void OnModelCreating(ModelBuilder modelBuilder)
        { }
        public DbSet<User> Users { get; set; }
    }
}
