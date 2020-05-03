using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class OrderDetailDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductDTO Product { get; set; }
    }
}
