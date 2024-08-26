using JobPostLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostLibrary.Repos
{
    public interface IJobPostRepoAsync
    {
         public Task AddJobPostAsync(JobPost jobPost);
         public Task UpdateJobPostAsync(int postId, JobPost jobPost);
         public Task RemoveJobPostAsync(int postId);
         public Task<JobPost> GetJobPostAsync(int postId);
         public Task<List<JobPost>> GetAllJobPostsAsync();
         public Task<List<JobPost>> GetJobPostsByJobIdAsync(string jobId);
         public Task AddJobAsync(Job job);   
         public Task RemoveJobAsync(string  jobId);
    }
}
