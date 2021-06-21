using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        void CreateRepository(CreateRepositoryModel input, string userId);
        ICollection<RepoViewModel> GetAllPublicRepositories();

    }
}
