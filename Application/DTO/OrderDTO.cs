using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalQuantity { get; set; }
        public IEnumerable<OrderDetailDTO> OrderDetails { get; set; }
    }
}
