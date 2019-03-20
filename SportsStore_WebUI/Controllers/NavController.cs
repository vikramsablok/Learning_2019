using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore_Domain.Abstract;
using System.Linq;

namespace SportsStore_WebUI.Controllers
{
    public class NavController : Controller
    {
        IProductRepository repository;
        public NavController(IProductRepository repo)
        {
            repository = repo;
        }
        // GET: Nav
        public PartialViewResult Menu(string category =null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Products
                                            .Select(x => x.Category)
                                            .Distinct()
                                            .OrderBy(x => x);
            return PartialView(categories);
                                        
        }   
    }
}