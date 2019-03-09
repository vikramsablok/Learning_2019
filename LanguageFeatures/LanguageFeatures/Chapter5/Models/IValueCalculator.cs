using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chapter5.Models
{
    public interface IValueCalculator
    {

        decimal ValueProducts(IEnumerable<Product> products);
    }
}