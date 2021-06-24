using Andreys.Models;
using Andreys.Services;
using Andreys.ViewModels.Products;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Controllers
{
    public class ProductsController:Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (String.IsNullOrWhiteSpace(input.Name) 
                || input.Name.Length < 4 || input.Name.Length > 20)
            {
                return this.Error("Name should be between 4 and 20 characters long.");
            }
            if (input.Description.Length > 10)
            {
                return this.Error("Description is too long.");
            }
            if (String.IsNullOrWhiteSpace(input.Price.ToString()))
            {
                return this.Error("Price Required");
            }
            if (String.IsNullOrWhiteSpace(input.Category.ToString())
                || !Enum.TryParse<Category>(input.Category, out _))
            {
                return this.Error("Invalid Category.");
            }
            if (String.IsNullOrWhiteSpace(input.Gender.ToString())
               || !Enum.TryParse<Gender>(input.Gender, out _))
            {
                return this.Error("Invalid Gender.");
            }

            this.productsService.AddProduct(input);

            return this.Redirect("/");
        }
        public HttpResponse Details(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var product = this.productsService.GetDetails(id);
            return this.View(product);
        }
        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            this.productsService.DeleteProduct(id);
            return this.Redirect("/");
        }
    }
}
