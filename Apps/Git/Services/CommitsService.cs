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

        public void CreateCommit(CommitInputModel input, string userId)
        {
            var commit = new Commit
            {
                 Description = input.Description,
                 CreatedOn = DateTime.UtcNow,
                 CreatorId = userId,
                 RepositoryId = input.Id,
            };
            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        

        public void DeleteCommit(string commitId)
        {
            var commit = this.db.Commits.Where(x => x.Id == commitId).FirstOrDefault();
            this.db.Commits.Remove(commit);
            this.db.SaveChanges();
        }

        public Commit GetCommit(string commitId)
        {
            return this.db.Commits.Where(x => x.Id == commitId).FirstOrDefault();
        }

        public ICollection<CommitViewModel> GetMyCommits(string userId)
        {
            var commits =  this.db.Commits
                .Where(x => x.CreatorId == userId)
                .Select(x => new CommitViewModel
                {
                   Id = x.Id,
                   Name = x.Repository.Name,
                   Description = x.Description,
                   CreatedOn = x.CreatedOn
                }).ToList();

            return commits;
        }

        public RepositoryViewModelForGetCommit GetRepositoryViewModel(string repositoryId)
        {
            return this.db.Repositories.Where(x => x.Id == repositoryId)
                .Select(x=> new RepositoryViewModelForGetCommit
                {
                    Name = x.Name,
                    Id = x.Id
                })
                .FirstOrDefault();
        }
    }
}
