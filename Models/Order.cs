using System;

namespace Advantage.API.Models{
    public class Order{
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderTotal { get; set; }

        public DateTime? Completed { get; set; }

    }
}
