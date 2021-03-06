using System;
using System.Collections.Generic;

namespace ShopApp.Model.Entity
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }       
        public string Url { get; set; }       
        public double? Price { get; set; } 
        public string Description { get; set; }         
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public DateTime DateAdded { get; set; }
        public List<ProductCategoryItem> ProductCategoryItems { get; set; }
    }
}