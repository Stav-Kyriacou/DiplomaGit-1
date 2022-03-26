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
        [Theory]
        [InlineData(0.5f, 1.5f, 2, 4)]
        [InlineData(0, 0, 0, 0)]
        [InlineData(0.5f, 0.5f, 0.5f, 1.5f)]
        [InlineData(-1.5f, -2.5f, 3.5f, -0.5f)]
        public void TotalValueTheory(float price1, float price2, float price3, float expected)
        {
            var products = new Products();
            products.ProductList = new List<Product>();
            products.ProductList.Add(new Product { Name = "Test", Price = price1, Qty = 1 });
            products.ProductList.Add(new Product { Name = "Test", Price = price2, Qty = 1 });
            products.ProductList.Add(new Product { Name = "Test", Price = price3, Qty = 1 });

            Assert.Equal(expected, products.GetTotalValueProducts());
        }
    }
}
