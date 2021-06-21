using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class CommitsController: Controller
    {
        private readonly ICommitsService commitsService;

        public CommitsController(ICommitsService commitsService)
        {
            this.commitsService = commitsService;
        }
        public HttpResponse All(string repositoryId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var allCommits = commitsService.GetAllCommits();
            return this.View(allCommits);
        }
        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }
    }
}
