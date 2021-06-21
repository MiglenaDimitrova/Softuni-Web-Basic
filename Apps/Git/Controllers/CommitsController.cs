using Git.Services;
using Git.ViewModels;
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
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var userId = this.GetUserId();
            var allCommits = commitsService.GetMyCommits(userId);
            return this.View(allCommits);
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var model = this.commitsService.GetRepositoryViewModel(id);
            return this.View(model);
        }

        [HttpPost]
        public HttpResponse Create(CommitInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            if (String.IsNullOrWhiteSpace(input.Description)|| input.Description.Length<5)
            {
                return this.Error("At least 5 characters required.");
            }

            var userId = this.GetUserId();
            this.commitsService.CreateCommit(input,userId);
            return this.Redirect("/Repositories/All");
        }
        
        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var userId = this.GetUserId();
            var commit = this.commitsService.GetCommit(id);
            var commitCreator = commit.CreatorId;
            if (commitCreator!=userId)
            {
                return this.Error("Unauthorized action!");
            }
            this.commitsService.DeleteCommit(id);
            return this.Redirect("/Commits/All");
        }
    }
}
