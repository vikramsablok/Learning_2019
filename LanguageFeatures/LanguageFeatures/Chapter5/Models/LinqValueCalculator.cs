using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chapter5.Models
{
    public class LinqValueCalculator : IValueCalculator
    {
        //solved with DI
        //public decimal ValueProducts(IEnumerable<Product> products)
        //{
        //    return products.Sum(p => p.Price);
        //}
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return products.Sum(p => p.Price);
        }
    }
}