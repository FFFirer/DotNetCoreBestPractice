using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaseKnowledgeApp.Services.PostgreSQL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString()
        {
            var orders = new StringBuilder();
            foreach (var o in Orders)
            {
                orders.Append(o.ToString());
            }

            return $"UserId:{Id} Name:{Name} Orders:{orders.ToString()}";
        }
    }
}
