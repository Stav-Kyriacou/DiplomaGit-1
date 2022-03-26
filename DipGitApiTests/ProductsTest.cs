using System;
using System.Collections.Generic;
using DipGitApiLib;
using Xunit;

namespace DipGitApiTests
{
    public class ProductsTest
    {
        [Theory]
        [InlineData(1, 2, 3, 6)]
        [InlineData(0, 0, 0, 0)]
        [InlineData(-1, -2, -3, -6)]
        [InlineData(-1, -2, 3, 0)]
        public void TotalQtyTheory(int qty1, int qty2, int qty3, int expected)
        {
            var products = new Products();
            products.ProductList = new List<Product>();
            products.ProductList.Add(new Product { Name = "Test", Price = 1.5f, Qty = qty1 });
            products.ProductList.Add(new Product { Name = "Test", Price = 1.5f, Qty = qty2 });
            products.ProductList.Add(new Product { Name = "Test", Price = 1.5f, Qty = qty3 });

            Assert.Equal(expected, products.GetTotalQtyProducts());
        }
    }
}
