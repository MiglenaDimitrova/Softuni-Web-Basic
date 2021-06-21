using Git.Services;
using Git.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class RepositoriesController: Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            var allPublicRepos = this.repositoriesService.GetAllPublicRepositories();
            if (!this.IsUserSignedIn())
            {
                return this.View(allPublicRepos);
            }
            var allRepos = this.repositoriesService.GetAllPrivateRepositories(this.GetUserId());
            allRepos.AddRange(allPublicRepos);
            return this.View(allRepos);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateRepositoryModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            if (String.IsNullOrWhiteSpace(input.Name)||input.Name.Length<3||input.Name.Length>10)
            {
                return this.Error("Name must be between 5 and 10 characters long");
            }
            //if (String.IsNullOrWhiteSpace(input.RepositoryType) || input.RepositoryType!="Public" || input.RepositoryType != "Private")
            //{
            //    return this.Error("Invalid Repository Type!");
            //}

            var userId = this.GetUserId();
            this.repositoriesService.CreateRepository(input, userId);
            return this.Redirect("/Repositories/All");
        }
    }
}
