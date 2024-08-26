using ApplyJobLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplyJobLibrary.Repos
{
    public interface IApplyJobRepoAsyn
    {
        public Task AddApplyJobAsync(ApplyJob applyJob);
        public Task UpdateApplyJobAsync(int postId, string empId, ApplyJob applyJob);
        public Task RemoveApplyJobAsync(int postId, string empId);
        public Task<ApplyJob> GetApplyJobAsync(int postId, string empId);
        public Task<List<ApplyJob>> GetAllApplyJobsAsync();
        public Task<List<ApplyJob>> GetApplyJobsByPostIdAsync(int postId);
        public Task<List<ApplyJob>> GetApplyJobsByEmpIdAsync(string empId);
        public Task AddJobPostAsync(JobPost jobPost);
        public Task RemoveJobPostAsync(int postId);
        public Task AddEmployeeAsync(Employee employee);
        public Task RemoveEmployeeAsync(string empId);
    }
}
