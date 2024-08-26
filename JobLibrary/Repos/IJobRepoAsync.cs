using JobLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLibrary.Repos
{
    public interface IJobRepoAsync
    {
        Task AddJobDetailsAsync(Job job);
        Task RemoveJobDetailsAsync(string jobId);
        Task UpdateJobDetailsAsync(string jobId, Job job);
        Task<Job> GetJobAsync(string jobId);
        Task<List<Job>> GetAllJobDetailsAsync();

    }
}
