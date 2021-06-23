using CarShop.Data;
using CarShop.Services;
using CarShop.ViewModels.Issues;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Controllers
{
    public class IssuesController: Controller
    {
        private readonly IIssuesService issuesService;

        public IssuesController(IIssuesService issuesService)
        {
            this.issuesService = issuesService;
        }
        public HttpResponse CarIssues(string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var model = this.issuesService.GetIssues(carId);
            return this.View(model);
        }

        public HttpResponse Add (string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View(carId);
        }

        [HttpPost]
        public HttpResponse Add(AddIssueInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            if (String.IsNullOrWhiteSpace(input.Description)|| input.Description.Length<5)
            {
                return this.Error("Description must be at least 5 characters long.");
            }

            this.issuesService.Add(input);

            return this.Redirect($"/Issues/CarIssues?carId={input.CarId}");
        }

        public HttpResponse Delete (string issueId, string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            this.issuesService.Delete(issueId, carId);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }

        public HttpResponse Fix(string issueId, string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            if (!this.issuesService.IsUserMechanic(this.GetUserId()))
            {
                return this.Error("Unauthorized action.");
            }

            this.issuesService.Fix(issueId, carId);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }
    }
}
