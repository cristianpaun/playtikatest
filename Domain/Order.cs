using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; }

        public string CartId { get; set; }

        public virtual IList<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
