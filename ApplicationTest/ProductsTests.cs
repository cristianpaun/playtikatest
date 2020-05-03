using Application.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationTest
{
    [TestClass]
    public class ProductsTests : TestBase
    {
        [TestMethod]
        public async Task ShouldReturnAllProductsInDb()
        {
            using (var context = GetDbContext())
            {
                //arrange
                InitProductsTable(context);
                var command = new List.Query();
                var handler = new List.Handler(context, GetMapper());

                //act
                var products = await handler.Handle(command, (new CancellationTokenSource()).Token);

                //assert
                Assert.IsTrue(products.Count == 3);
            }
        }

        private void InitProductsTable(DataContext context)
        {
            context.Products.Add(new Domain.Product() { Id = 1, Name = "Apples" });
            context.Products.Add(new Domain.Product() { Id = 2, Name = "Carrots" });
            context.Products.Add(new Domain.Product() { Id = 3, Name = "Beans" });
            context.SaveChanges();
        }
    }
}
