using CarShop.ViewModels.Issues;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Services
{
    public interface IIssuesService
    {
        AllIssuesViewModel GetIssues(string carId);
        void Add(AddIssueInputModel input);
        void Delete(string issueId, string carId);
        void Fix(string issueId, string carId);
        bool IsUserMechanic(string userId);
    }
}
