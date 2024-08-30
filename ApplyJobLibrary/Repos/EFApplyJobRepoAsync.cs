using ApplyJobLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplyJobLibrary.Repos
{
    public class EFApplyJobRepoAsync : IApplyJobRepoAsyn
    {
        ApplyJobDBContext ctx = new ApplyJobDBContext();
        public async Task AddApplyJobAsync(ApplyJob applyJob)
        {
            try
            {
                await ctx.ApplyJobs.AddAsync(applyJob);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new ApplyJobException(ex.InnerException.Message);
            }
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                await ctx.Employees.AddAsync(employee);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplyJobException(ex.Message);
            }
        }
        public async Task RemoveEmployeeAsync(string empId)
        {
            try
            {
                Employee employee = await(from e in ctx.Employees where e.EmpId == empId select e).FirstAsync();
                ctx.Employees.Remove(employee);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplyJobException(ex.InnerException.Message);
            }
        }

        public async Task RemoveJobPostAsync(int postId)
        {
            try
            {
                JobPost jobPost = await(from jp in ctx.JobPosts where jp.PostId == postId select jp).FirstAsync();
                ctx.JobPosts.Remove(jobPost);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplyJobException(ex.InnerException.Message);
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
                throw new ApplyJobException(ex.Message);
            }
        }

        public async Task<List<ApplyJob>> GetAllApplyJobsAsync()
        {
            List<ApplyJob> appliedJobs = await ctx.ApplyJobs.ToListAsync();
            return appliedJobs;
        }

        public async Task<ApplyJob> GetApplyJobAsync(int postId, string empId)
        {
            try
            {
                ApplyJob appliedJob = await(from ap in ctx.ApplyJobs where ap.PostId == postId && ap.EmpId == empId select ap).FirstAsync();
                return appliedJob;
            }
            catch
            {
                throw new ApplyJobException("No Application found");
            }
        }

        public async Task<List<ApplyJob>> GetApplyJobsByEmpIdAsync(string empId)
        {
            try
            {
                List<ApplyJob> appliedJobs = await(from ap in ctx.ApplyJobs where  ap.EmpId == empId select ap).ToListAsync();
                return appliedJobs;
            }
            catch
            {
                throw new ApplyJobException("No Applications found for this employee id");
            }
        }

        public async Task<List<ApplyJob>> GetApplyJobsByPostIdAsync(int postId)
        {
            try
            {
                List<ApplyJob> appliedJobs = await(from ap in ctx.ApplyJobs where ap.PostId == postId select ap).ToListAsync();
                return appliedJobs;
            }
            catch
            {
                throw new ApplyJobException("No Applications found for this post id");
            }
        }

        public async Task RemoveApplyJobAsync(int postId, string empId)
        {
            try
            {
                ApplyJob appliedJob = await GetApplyJobAsync(postId,empId);
                ctx.ApplyJobs.Remove(appliedJob);
                await ctx.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new ApplyJobException(ex.Message);
            }
        }
        public async Task UpdateApplyJobAsync(int postId, string empId, ApplyJob applyJob)
        {
            try
            {
                ApplyJob appliedJob2 = await GetApplyJobAsync(postId, empId);
                appliedJob2.AppliedDate = applyJob.AppliedDate;
                appliedJob2.ApplicationStatus = applyJob.ApplicationStatus;
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplyJobException(ex.Message);
            }
        }
    }
}
