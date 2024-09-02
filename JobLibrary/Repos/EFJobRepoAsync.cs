using JobLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLibrary.Repos
{
    public class EFJobRepoAsync : IJobRepoAsync
    {
        JobDBContext ctx=new JobDBContext();
        public async Task AddJobDetailsAsync(Job job)
        {
            try
            {
                await ctx.Jobs.AddAsync(job);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new JobException(ex.InnerException.Message);

            }
        }

        public async Task<List<Job>> GetAllJobDetailsAsync()
        {
            List<Job> jobs=await ctx.Jobs.ToListAsync();
            return jobs;
        }

        public async Task<Job> GetJobAsync(string jobId)
        {
            try
            {
                Job job = await (from j in ctx.Jobs where j.JobId == jobId select j).FirstAsync();
                return job;
            }
            catch
            {
                throw new JobException("No Such JobId Found");
            }
        }

        public async Task RemoveJobDetailsAsync(string jobId)
        {
            Job job=await GetJobAsync(jobId);
            try
            {
                ctx.Jobs.Remove(job);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new JobException(ex.InnerException.Message);
            }
        }

        public async Task UpdateJobDetailsAsync(string jobId, Job job)
        {
            try
            {
                Job j = await GetJobAsync(jobId);
                j.JobTitle = job.JobTitle;
                j.JobDescription = job.JobDescription;
                j.Salary = job.Salary;
                await ctx.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new JobException(ex.Message);
            }
        }
    }
}
