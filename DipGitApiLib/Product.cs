using System;
using System.Collections.Generic;
using System.Linq;

namespace DipGitApiLib
{
    public class Product
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Qty { get; set; }
    }

    public class Products
    {
        public List<Product> ProductList { get; set; }
        /// <summary>
        /// Sums the qty of all items in ProductList together
        /// </summary>
        /// <returns></returns>
        public int GetTotalQtyProducts()
        {
            int QtyTotal = 0;

            foreach (Product Product in ProductList)
            {
                QtyTotal += Product.Qty;
            }
            return QtyTotal;
        }

        /// <summary>
        /// Gets the total cost of inventory, that is the sum of the cost of all items 
        /// </summary>
        /// <returns></returns>
        public float GetTotalValueProducts()
        {
            float QtyTtlValue = 0;

            foreach (Product Product in ProductList)
            {
                QtyTtlValue += (Product.Price * Product.Qty);
            }
            return QtyTtlValue;
        }
    }
}
