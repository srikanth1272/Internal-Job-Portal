using JobPostLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostLibrary.Repos
{
    public class EFJobPostRepoAsync : IJobPostRepoAsync
    {
        JobPostDBContext ctx = new JobPostDBContext();
        public async Task AddJobAsync(Job job)
        {
            try
            {
                await ctx.Jobs.AddAsync(job);
                await ctx.SaveChangesAsync();

            }
            catch (Exception ex) {
                throw new JobPostException(ex.InnerException.Message);
            }
        }
        public async Task RemoveJobAsync(string jobId)
        {
            try
            {
                Job job = await (from j in ctx.Jobs where j.JobId == jobId select j).FirstAsync();
                ctx.Jobs.Remove(job);
                await ctx.SaveChangesAsync();
            }
            catch(Exception ex){
                throw new JobPostException(ex.InnerException.Message);
            }
        }
        public async Task AddJobPostAsync(JobPost jobPost)
        {
            try
            {
                await ctx.JobPosts.AddAsync(jobPost);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new JobPostException(ex.Message);
            }
        }

        public async Task<List<JobPost>> GetAllJobPostsAsync()
        {
            List<JobPost> jobPosts = await ctx.JobPosts.ToListAsync();
            return jobPosts;
        }

        public async Task<JobPost> GetJobPostAsync(int postId)
        {
            try { 
                JobPost jobPost = await (from jp  in ctx.JobPosts where jp.PostId == postId select jp).FirstAsync(); 
                return jobPost;
            }
            catch {
                throw new JobPostException("No JobPost found for the postid");
            }
        }

        public async Task<List<JobPost>> GetJobPostsByJobIdAsync(string jobId)
        {
            try
            {
                List<JobPost> jobPosts = await(from jp in ctx.JobPosts where jp.JobId == jobId select jp).ToListAsync();
                return jobPosts;
            }
            catch
            {
                throw new JobPostException("No JobPosts found for the jobid");
            }
        }

        public async Task RemoveJobPostAsync(int postId)
        {
            try{
                JobPost jobPost = await GetJobPostAsync(postId);
                ctx.JobPosts.Remove(jobPost);
                await ctx.SaveChangesAsync();   
            }
            catch(Exception ex){
                throw new JobPostException(ex.InnerException.Message);
            }
        }

        public async Task UpdateJobPostAsync(int postId, JobPost jobPost)
        {
            try
            {
                JobPost jobPost2 = await GetJobPostAsync(postId);
                jobPost2.JobId = jobPost.JobId;
                jobPost2.DateofPosting = jobPost.DateofPosting;
                jobPost2.LastDatetoApply = jobPost.LastDatetoApply;
                jobPost2.Vacancies = jobPost.Vacancies;
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new JobPostException(ex.Message);
            }
        }
    }
}
