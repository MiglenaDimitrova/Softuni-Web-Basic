using Git.Data;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;
        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateCommit(string userId, string repositoryId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommit(string commiteId)
        {
            throw new NotImplementedException();
        }

        public ICollection<CommitViewModel> GetMyCommits(string userId)
        {
            return this.db.Commits
                .Where(x => x.CreatorId == userId)
                .Select(x => new CommitViewModel
                {
                   RepositoryName = x.Repository.Name,
                   Description = x.Description,
                   CreatedOn = x.CreatedOn
                }).ToList();
        }
    }
}
