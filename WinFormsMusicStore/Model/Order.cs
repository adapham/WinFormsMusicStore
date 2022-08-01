using System;
using System.Collections.Generic;

#nullable disable

namespace WinFormsMusicStore.Model
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public double? Total { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
