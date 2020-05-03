using Application.DTO;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Orders
{
    public class AddNewOrder
    {
        public class Command : IRequest
        {
            public string CartId { get; set; }

            public IEnumerable<OrderDetailDTO> ProductsList { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await CheckIfOrderIsValid(request);                
                Order order = AddOrder(request);
                AddOrderDetails(request, order);

                var success = await _context.SaveChangesAsync() > 0;

                return success ? Unit.Value : throw new Exception("Error saving order!");
            }

            private Order AddOrder(Command request)
            {
                var order = new Order()
                {
                    CartId = request.CartId,
                    OrderDate = DateTime.Now
                };
                _context.Orders.Add(order);
                return order;
            }

            private void AddOrderDetails(Command request, Order order)
            {
                var products = new List<OrderDetail>();
                foreach (var product in request.ProductsList)
                {
                    var productInCart = new OrderDetail() { Order = order, OrderId = order.Id, ProductId = product.ProductId, Quantity = product.Quantity };
                    products.Add(productInCart);
                }
                _context.OrderDetails.AddRange(products);
            }

            private async Task CheckIfOrderIsValid(Command request)
            {
                var p = await _context.Products.Where(p => request.ProductsList.Select(x => x.ProductId).Contains(p.Id)).ToListAsync();
                var invalidProductsQuery = from prod in p
                                           join orderList in request.ProductsList on prod.Id equals orderList.ProductId
                                           where prod.Name.Length < orderList.Quantity
                                           select new { prod.Name, orderList.Quantity };


                var invalidProducts = invalidProductsQuery.ToList();
                if (invalidProductsQuery.Count() > 0)
                    throw new RestException(HttpStatusCode.BadRequest, string.Join(", ", invalidProducts.Select(p => $"{p.Name} qty({p.Quantity}) > {p.Name.Length}")));
            }
        }
    }
}
