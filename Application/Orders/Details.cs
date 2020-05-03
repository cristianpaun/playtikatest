using Application.DTO;
using Application.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Orders
{
    public class Details
    {
        public class Query : IRequest<OrderDTO>
        {
            public string CartId { get; set; }            
        }

        public class Handler : IRequestHandler<Query, OrderDTO>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<OrderDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.Where(o => o.CartId == request.CartId).FirstOrDefaultAsync();
                if (order == null)
                    throw new RestException(HttpStatusCode.NotFound, $"There is no order with number {request.CartId} in the database!");

                var orderToReturn = new OrderDTO()
                {
                    CartId = order.CartId,
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    TotalQuantity = order.OrderDetails.Sum(od => od.Quantity),
                    OrderDetails = order.OrderDetails.Select(o => new OrderDetailDTO() { ProductId = o.ProductId, Product = new ProductDTO() { Key = o.ProductId, Name = o.Product.Name }, Quantity = o.Quantity })
                };

                return orderToReturn;
            }
        }
    }
}
