using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShop.Data.Models
{
    public class Issue
    {
        public Issue()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }
        public bool IsFixed { get; set; }

        [Required]
        public string CarId { get; set; }
        public Car Car { get; set; }
    }
}
