using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaseKnowledgeApp.Services.PostgreSQL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
        public override string ToString()
        {
            return $"OrderId:{Id} Item:{Item} Description:{Description}";
        }
    }
}
