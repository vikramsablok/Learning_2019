using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chapter5.Models;
using Ninject;

namespace Chapter5.Controllers
{
    public class ProductController : Controller
    {
        private Product[] products = {
            new Product { Name = "Kayak", Category = "Watersports", Price = 275M },
            new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95M },
            new Product { Name = "Soccer ball", Category = "Soccer", Price = 19.50M },
            new Product { Name = "Corner flag", Category = "Soccer", Price = 34.95M }
        };

        Product myProduct = new Product()
        {
            ProductID = 1,
            Name = "Kayak",
            Description = "A boat for one person",
            Category = "Watersports",
            Price = 275M
        };
        // GET: Product
        public ActionResult Index()
        {
            return View(myProduct);
        }
        public ActionResult NameandPrice()
        {
            return View(myProduct);
        }
        public ActionResult FormattedOutput()
        {
            ViewBag.ProductCount = 1;
            return View(myProduct);
        }
        public ActionResult DemoArray()
        {
            Product[] array = { new Product { Name = "Kayak", Price = 275M },
                new Product { Name = "Lifejacket", Price = 48.95M },
                new Product { Name = "Soccer ball", Price = 19.50M },
                new Product { Name = "Corner flag", Price = 34.95M }
            };
            return View(array);
        }
        public ActionResult ProductTotal()
        {
            IValueCalculator calc = new LinqValueCalculator();
            ShoppingCart cart = new ShoppingCart(calc)
            { Products = products };
            decimal totalval = cart.CalculateProductTotal();

            return View(totalval);
        }
        //Product total using Ninject
        /*
         Install-Package Ninject -version 3.0.1.10 
         Install-Package Ninject.Web.Common -version 3.0.0.7 
         Install-Package Ninject.MVC3 -Version 3.0.0.6
    */
        public ActionResult ProductTotalUsingNinject()
        {
            IKernel ninjectKernal = new StandardKernel();
            ninjectKernal.Bind<IValueCalculator>().To<LinqValueCalculator>();
            IValueCalculator calc = ninjectKernal.Get<IValueCalculator>();
            ShoppingCart cart = new ShoppingCart(calc)
            { Products = products };
            decimal totalval = cart.CalculateProductTotal();
            return View(totalval);
        }
    }
}
