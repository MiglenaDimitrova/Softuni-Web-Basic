using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Services
{
    public interface IProductsService
    {
        void AddProduct(AddInputModel input);
        ICollection<ProductViewModel> GetAllProducts();
        DetailsProductViewModel GetDetails(string id);
        void DeleteProduct(string id);
    }
}
