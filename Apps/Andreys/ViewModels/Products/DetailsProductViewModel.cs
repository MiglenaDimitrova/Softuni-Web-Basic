using Andreys.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.ViewModels.Products
{
    public class DetailsProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }
    }
}
