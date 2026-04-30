using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public List<Order> Orders { get; set; }
    }
}