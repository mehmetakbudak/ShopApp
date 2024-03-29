using Microsoft.EntityFrameworkCore;
using ShopApp.Data.GenericRepository;
using ShopApp.Model.Dto;
using ShopApp.Model.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Business.Services
{
    public interface IProductCategoryService
    {
        ProductCategory GetById(int id);

        ProductCategory GetByIdWithProducts(int categoryId);

        List<TreeProductCategoryModel> GetAll(int? parentId = null);

        void Create(ProductCategory entity);

        void Update(ProductCategory entity);

        void Delete(ProductCategory entity);

        void DeleteFromCategory(int productId, int categoryId);
    }


    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(ProductCategory entity)
        {
            _unitOfWork.Repository<ProductCategory>().Add(entity);
            _unitOfWork.Save();
        }

        public void Delete(ProductCategory entity)
        {
            _unitOfWork.Repository<ProductCategory>().Delete(entity);
            _unitOfWork.Save();
        }

        public void DeleteFromCategory(int productId, int categoryId)
        {
            var productCategoryItems = _unitOfWork
                .Repository<ProductCategoryItem>()
                .FirstOrDefault(x => x.ProductCategoryId == categoryId && x.ProductId == productId);

            if (productCategoryItems != null)
            {
                _unitOfWork.Repository<ProductCategoryItem>().Delete(productCategoryItems);
                _unitOfWork.Save();
            }
        }

        public List<TreeProductCategoryModel> GetAll(int? parentId = null)
        {
            var productCategories = new List<TreeProductCategoryModel>();
            List<TreeProductCategoryModel> list = _unitOfWork.Repository<ProductCategory>()
                .Where(x => x.ParentId == parentId)
                .AsEnumerable()
                .Select(x => 
                {
                    var parents = new List<int?>();
                    var model = new TreeProductCategoryModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ParentId = parents,
                        Url = x.Url
                    };
                    return model;
                }).ToList();

            foreach (var item in list)
            {
                var items = GetAll(item.Id);
                if (items.Count > 0)
                {
                    item.Items = items;
                    item.ParentId.Add(item.Id);
                }
            }

            productCategories.AddRange(list);

            return productCategories;
        }

        public ProductCategory GetById(int id)
        {
            return _unitOfWork.Repository<ProductCategory>().FirstOrDefault(x => x.Id == id);
        }

        public ProductCategory GetByIdWithProducts(int categoryId)
        {
            var category = _unitOfWork.Repository<ProductCategory>()
                .Where(i => i.Id == categoryId)
                .Include(i => i.ProductCategoryItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault();
            return category;
        }

        public void Update(ProductCategory entity)
        {
            _unitOfWork.Repository<ProductCategory>().Update(entity);
            _unitOfWork.Save();
        }
    }
}