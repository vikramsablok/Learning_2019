using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LanguageFeatures.Models;
using System.Text;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public string Index()
        {

            return "Navigate to a URL to show an example";
        }
        public ViewResult AutoProperty()
        {
            Product p = new Product();
            p.Name = "Kayak";
            p.Price = 100;
            
            return View("Result", (object)String.Format("Product name: {0} ", p.Name));

        }
        public ViewResult CreateProduct()
        {

            // create a new Product object            
            //Product myProduct = new Product();

            // set the property values           

            //myProduct.ProductID = 100;
            //myProduct.Name = "Kayak";
            //myProduct.Description = "A boat for one person";
            //myProduct.Price = 275M;
            //myProduct.Category = "Watersports";

            // create and populate a new Product object using object initializer
            Product myProduct = new Product
            {
                ProductID = 100,
                Name = "Kayak",
                Description = "A boat for one person",

                Price = 275M,
                Category = "Watersports"
            };


            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));
        }

        public ViewResult CreateCollection()
        {
            string[] stringArray = { "apple", "orange", "plum" };

            List<int> intList = new List<int> { 10, 20, 30, 40 };

            Dictionary<string, int> myDict = new Dictionary<string, int> {
                { "apple", 10 },
                { "orange", 20 },
                { "plum", 30 },
                {"Mango" ,40}
            };

            return View("Result", (object)myDict["Mango"]);
        }
        public ViewResult UseExtenstionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart()
            {
                Products = new List<Product> {
                    new Product {Name = "Kayak", Price = 275M},
                    new Product {Name = "Lifejacket", Price = 48.95M},
                    new Product {Name = "Soccer ball", Price = 19.50M},
                    new Product {Name = "Corner flag", Price = 34.95M}
                }
            };

            // create and populate an array of Product objects           

            Product[] productArray = {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };

            // get the total value of the products in the cart          
            decimal cartTotal = products.TotalPrices();            
            decimal arrayTotal = productArray.TotalPrices();
            

            return View("Result", (object)String.Format("Cart Total: {0}, Array Total: {1}", cartTotal, arrayTotal));

        }
        public ViewResult UseExtensionCategory()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product> {
                    new Product { Name = "Kayak", Category = "Watersports", Price = 275M },
                    new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95M },
                    new Product { Name = "Soccer ball", Category = "Soccer", Price = 19.50M },
                    new Product { Name = "Corner flag", Category = "Soccer", Price = 34.95M }
                }
            };

            decimal total = 0; foreach (Product prod in products.FilterByCategory("Soccer"))
            { total += prod.Price; }

            return View("Result", (object)String.Format("Total: {0}", total));
        }
        public ViewResult UseExtensionFilters()
        {
            // create and populate ShoppingCart
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product> {
                new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
                }
            };
            //Func<Product, bool> categoryFilter = delegate (Product prod)
            //{
            //    return prod.Category == "Soccer";
            //};
            //using lambda expression
            Func<Product, bool> categoryFilter = prod => prod.Category == "Soccer";
            decimal total = 0;
            foreach (Product prod in products.Filter(categoryFilter))
            {
                total += prod.Price;
            }
            return View("Result", (object)String.Format("Total Scoccer Filtered value {0}", total));

        }
        public ViewResult FindTop3Products()
        {
            Product[] products = {
                new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
                };
            // define the array to hold the results
            //Product[] foundproducts = new Product[3];
            var foundproducts = products.OrderByDescending(e => e.Price).Take(3).Select(e=> new { e.Name, e.Price });

                                
                            
            //Array.Sort(products, (item1, item2) =>
            //{
            //    return Comparer<decimal>.Default.Compare(item1.Price, item2.Price);
            //});
            //Array.Copy(products, foundproducts,3);

            // create the result
            StringBuilder result = new StringBuilder();
            foreach (var p in foundproducts)
            {
                result.AppendFormat("Price: {0} \n", p.Price);
            }
            return View("Result", (object)String.Format( result.ToString()));
        }
        public ViewResult SumProducts()
        {

            Product[] products = { new Product { Name = "Kayak", Category = "Watersports", Price = 275M },
                new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95M },
                new Product { Name = "Soccer ball", Category = "Soccer", Price = 19.50M },
                new Product { Name = "Corner flag", Category = "Soccer", Price = 34.95M } };

            var results = products.Sum(e => e.Price);


            products[2] = new Product { Name = "Stadium", Price = 79500M };

            return View("Result", (object)String.Format("Sum: {0:c}", results));
        }
    }
}