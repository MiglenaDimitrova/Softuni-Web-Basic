using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.ViewModels.Issues
{
    public class IssueViewModel
    {
        public string CarId { get; set; }
        public string Description { get; set; }
        public string IsItFixed { get; set; }
        public string IssueId { get; set; }
    }
}
