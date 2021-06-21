using Git.Data;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface ICommitsService
    {
        Commit GetCommit(string commitId); 
        void CreateCommit(CommitInputModel input, string userId);
        RepositoryViewModelForGetCommit GetRepositoryViewModel(string repositoryId);
        ICollection<CommitViewModel> GetMyCommits(string userId);
        void DeleteCommit(string commitId);
    }
}
