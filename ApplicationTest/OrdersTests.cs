using Application.DTO;
using Application.Errors;
using Application.Orders;
using Application.Products;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationTest
{
    [TestClass]
    public class OrdersTests : TestBase
    {
        [TestMethod]
        public async Task SearchByValidCartIdShouldReturnOrder()
        {
            using (var context = GetDbContext())
            {
                //arrange
                InitDatabaseTablesForOrderTests(context);
                var command = new Details.Query() { CartId = "1234-5678-0000-1111" };
                var handler = new Details.Handler(context);

                //act
                var order = await handler.Handle(command, (new CancellationTokenSource()).Token);

                //assert
                Assert.IsTrue(order.CartId == "1234-5678-0000-1111" && order.OrderDetails.Count() == 3 && order.Id ==1);
            }
        }

        [TestMethod]
        public void SearchByInvalidCartIdShouldThrowRestException()
        {
            using (var context = GetDbContext())
            {
                //arrange
                InitDatabaseTablesForOrderTests(context);
                var command = new Details.Query() { CartId = "1234-5678-0000-3333" };
                var handler = new Details.Handler(context);

                //assert
                Assert.ThrowsExceptionAsync<RestException>(() => handler.Handle(command, (new CancellationTokenSource()).Token));
            }
        }

        [TestMethod]
        public async Task AddValidOrderShouldPersistedInDatabase()
        {
            using (var context = GetDbContext())
            {
                //arrange
                InitDatabaseTablesForOrderTests(context);
                
                var command = new AddNewOrder.Command() { CartId = "1234-5678-0000-4444", 
                        ProductsList = new List<OrderDetailDTO>() {
                            new OrderDetailDTO() { ProductId=1, Quantity=1 },
                            new OrderDetailDTO() { ProductId=2, Quantity=2 },
                        } 
                };
                var handler = new AddNewOrder.Handler(context);

                //act
                await handler.Handle(command, (new CancellationTokenSource()).Token);

                //assert
                Assert.IsTrue(context.Orders.Any(o => o.CartId == "1234-5678-0000-4444"));
            }
        }

        [TestMethod]
        public void AddInvalidOrderShouldThrowRestException()
        {
            using (var context = GetDbContext())
            {
                //arrange
                InitDatabaseTablesForOrderTests(context);

                var command = new AddNewOrder.Command()
                {
                    CartId = "1234-5678-0000-4444",
                    ProductsList = new List<OrderDetailDTO>() {
                            new OrderDetailDTO() { ProductId=1, Quantity=10 },
                            new OrderDetailDTO() { ProductId=2, Quantity=2 },
                        }
                };
                var handler = new AddNewOrder.Handler(context);

                //assert
                Assert.ThrowsExceptionAsync<RestException>(() => handler.Handle(command, (new CancellationTokenSource()).Token));
            }
        }

        public void InitDatabaseTablesForOrderTests(DataContext context)
        {
            context.Products.Add(new Domain.Product() { Id = 1, Name = "Apples" });
            context.Products.Add(new Domain.Product() { Id = 2, Name = "Carrots" });
            context.Products.Add(new Domain.Product() { Id = 3, Name = "Beans" });

            context.Orders.Add(new Domain.Order() { Id = 1, CartId = "1234-5678-0000-1111", OrderDate = DateTime.Now });
            context.Orders.Add(new Domain.Order() { Id = 2, CartId = "1234-5678-0000-2222", OrderDate = DateTime.Now });

            context.OrderDetails.Add(new Domain.OrderDetail() { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1 });
            context.OrderDetails.Add(new Domain.OrderDetail() { Id = 2, OrderId = 1, ProductId = 2, Quantity = 2 });
            context.OrderDetails.Add(new Domain.OrderDetail() { Id = 3, OrderId = 1, ProductId = 3, Quantity = 3 });
            context.OrderDetails.Add(new Domain.OrderDetail() { Id = 4, OrderId = 2, ProductId = 1, Quantity = 2 });

            context.SaveChanges();
        }
    }
}
