using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Issues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarShop.Services
{
    public class IssuesService : IIssuesService
    {
        private readonly ApplicationDbContext db;

        public IssuesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public AllIssuesViewModel GetIssues(string carId)
        {
            var issues =   this.db.Issues.Where(x => x.CarId == carId)
                .Select(x => new IssueViewModel
                {
                    Description = x.Description,
                    IsItFixed = x.IsFixed?"Yes":"Not Yet",
                    CarId = x.CarId,
                    IssueId = x.Id
                }).ToList();

           var model = new AllIssuesViewModel
            {
                CarId = carId,
                Issues = issues
            };
            return model;
        }

        public void Add(AddIssueInputModel input)
        {
            var issue = new Issue
            {
                CarId = input.CarId,
                Description = input.Description,
                IsFixed = false,
            };
            this.db.Issues.Add(issue);
            this.db.SaveChanges();
        }

        public void Delete(string issueId, string carId)
        {
            var issue = this.db.Issues.FirstOrDefault(x => x.Id == issueId && x.CarId == carId);
            this.db.Issues.Remove(issue);
            this.db.SaveChanges();
        }
        public bool IsUserMechanic(string userId)
        {
            var user = this.db.Users.Where(x => x.Id == userId).FirstOrDefault();
            return user.IsMechanic;
        }

        public void Fix(string issueId, string carId)
        {
            var issue = this.db.Issues.FirstOrDefault(x => x.Id == issueId && x.CarId == carId);
            issue.IsFixed = true;
            this.db.SaveChanges();
        }
    }
}
