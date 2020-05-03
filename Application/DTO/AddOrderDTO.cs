using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class AddOrderDTO
    {
        public string CartId { get; set; }
        public IEnumerable<OrderDetailDTO> Products { get; set; }
    }
}
