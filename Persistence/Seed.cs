using Domain;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Persistence
{
    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            if (context.Products.Any())
            {
                return;
            }
            var products = new List<Product>()
            {
                new Product() { Id = 1, Name = "Carrots"},
                new Product() { Id = 2, Name = "Beans"},
                new Product() { Id = 3, Name = "Potatoes"},
                new Product() { Id = 4, Name = "Dill"},
                new Product() { Id = 5, Name = "Cucumbers"}
            };
            context.AddRange(products);
            context.SaveChanges();
        }
    }
}
