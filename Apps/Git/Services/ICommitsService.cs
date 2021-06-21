using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface ICommitsService
    {
        void AddCommite(string userId, string repositoryId);
        ICollection<CommiteViewModel> GetMyCommits(string userId);
        void DeleteCommite(string commiteId);
    }
}
