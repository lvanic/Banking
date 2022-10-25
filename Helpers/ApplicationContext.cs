using Banking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Helpers
{
    class ApplicationContext : DbContext
    {
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<BillModel> Bills { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SupportModel> Supports { get; set; }
        public DbSet<Request> Requests { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .\\SQLEXPRESS; Database = bankingdb; Trusted_Connection = True;");
        }

    }
}
