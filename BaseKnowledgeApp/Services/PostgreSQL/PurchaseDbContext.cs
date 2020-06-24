using BaseKnowledgeApp.Services.PostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseKnowledgeApp.Services.PostgreSQL
{
    public class PurchaseDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=172.16.98.213;Username=postgres;Password=stk!2018;Database=Purchase");
        }
    }
}
