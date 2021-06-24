using Andreys.Data;
using Andreys.Models;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andreys.Services
{
    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }
        public void AddProduct(AddInputModel input)
        {
            var product = new Product
            {
                 Name = input.Name,
                 Description = input.Description,
                 ImageUrl = input.ImageUrl,
                 Price = input.Price,
                 Category = Enum.Parse<Category>(input.Category),
                 Gender = Enum.Parse<Gender>(input.Gender),
            };
            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public void DeleteProduct(string id)
        {
            var product = this.db.Products
                .FirstOrDefault(x => x.Id.ToString() == id);
            this.db.Products.Remove(product);
            this.db.SaveChanges();
        }

        public ICollection<ProductViewModel> GetAllProducts()
        {
            return  this.db.Products
                .Select(x => new ProductViewModel
                {
                    Id = x.Id.ToString(),
                    ImageUrl = x.ImageUrl,
                    Name = x.Name,
                    Price = x.Price.ToString("F2"),
                }).ToList();
        }
        public DetailsProductViewModel GetDetails(string id)
        {
            return  this.db.Products
                .Where(x => x.Id.ToString() == id)
                .Select(x => new DetailsProductViewModel
                {
                    Id = id,
                    Category = x.Category.ToString(),
                    Description = x.Description,
                    Gender = x.Gender.ToString(),
                    ImageUrl = x.ImageUrl,
                    Name = x.Name,
                    Price = x.Price.ToString("F2")
                }).FirstOrDefault();
        }
    }
}
