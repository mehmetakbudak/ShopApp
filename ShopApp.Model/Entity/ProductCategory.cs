using System.Collections.Generic;

namespace ShopApp.Model.Entity
{
    public class ProductCategory : BaseEntity
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<ProductCategoryItem> ProductCategoryItems { get; set; }

    }
}