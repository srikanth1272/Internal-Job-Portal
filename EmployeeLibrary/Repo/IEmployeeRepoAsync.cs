using EmployeeLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillLibrary.Repo
{
    public interface IEmployeeRepoAsync
    {
        Task AddEmployeeDetailsAsync(Employee employee);
        Task RemoveEmployeeDetailsAsync(string empId);
        Task UpdateEmployeeDetailsAsync(string empId, Employee employee);
        Task<Employee> GetEmployeeAsync(string empId);
        Task<List<Employee>> GetAllEmployeeDetailsAsync();
        Task<List<Employee>> GetEmpByJobIdAsync(string jobId);
        Task AddJobAsync(Job job);
        Task RemoveJobAsync(string jobid);

    }
}
