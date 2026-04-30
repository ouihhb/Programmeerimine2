using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class Order : Entity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}