using Git.Data;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;
        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateRepository(CreateRepositoryModel input, string userId)
        {
            var repository = new Repository
            {
                 Name = input.Name,
                 IsPublic = input.RepositoryType=="Public" ? true : false,
                 OwnerId = userId,
                 CreatedOn = DateTime.UtcNow,
            };
            this.db.Repositories.Add(repository);
            this.db.SaveChanges();
        }

        public ICollection<RepoViewModel> GetAllPublicRepositories()
        {
            return this.db.Repositories
                .Where(x => x.IsPublic == true)
                .Select(x => new RepoViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    Owner = x.Owner.Username,
                    CommitsCount= x.Commits.Count
                }).ToList();
        }
    }
}
