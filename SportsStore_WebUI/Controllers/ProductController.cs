﻿using SportsStore_Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore_WebUI.Models;

namespace SportsStore_WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }
        public ViewResult List(string category,int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category==null?
                        repository.Products.Count():
                        repository.Products.Where(e=>e.Category==category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
            
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}