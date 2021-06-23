using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.ViewModels.Issues
{
    public class AllIssuesViewModel
    {
        public string CarId { get; set; }
        public ICollection<IssueViewModel> Issues { get; set; }
    }
}
