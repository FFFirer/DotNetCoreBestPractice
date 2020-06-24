using BaseKnowledgeApp.Services.PostgreSQL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseKnowledgeApp.Services.PostgreSQL
{
    public class PurchaseDbContextSeeder
    {
        public static void Seed(PurchaseDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var users = new List<User>
            { 
                new User {Name = "Tom"},
                new User{Name="Mary"}
            };

            var orders = new List<Order>
            {
                new Order
                {
                    User=users[0],
                    Item="cloth",
                    Description="handsome"
                },
                new Order
                {
                    User=users[1],
                    Item="hat",
                    Description="red"
                },
                new Order
                {
                    User=users[1],
                    Item="boot",
                    Description="black"
                }
            };

            context.Users.AddRange(users);
            context.Orders.AddRange(orders);

            context.SaveChanges();
        }
    }
}
