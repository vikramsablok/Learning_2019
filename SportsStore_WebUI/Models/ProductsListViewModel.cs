using SportsStore_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore_WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        //FROM HTML HELPER
        public PagingInfo PagingInfo
        {
            get; set;
        }
        public string CurrentCategory { get; set; }
    }
}