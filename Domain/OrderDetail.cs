﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderDetail : Entity
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
