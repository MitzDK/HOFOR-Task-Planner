using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HOFORTaskPlanner.Models
{
    public class TaskPlannerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                @"Data Source=mssql14.unoeuro.com;Initial Catalog=boddelboys_dk_db_plannerdb;User ID=boddelboys_dk;Password=EbR5dmpyz4F6;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        public DbSet<Aktion> Aktions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<TimeReg> Times { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
