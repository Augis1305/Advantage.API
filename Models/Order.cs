using System;

namespace Advantage.API.Models{
    public class Order{
        public int Id { get; set; }
        public Customer Customer { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime? Completed { get; set; }

        public DateTime Placed { get; set; }

    }
}


