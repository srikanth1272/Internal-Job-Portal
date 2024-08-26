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
        Task AddEmployeeDetails(Employee employee);
        Task RemoveEmployeeDetails(string empId);
        Task UpdateEmployeeDetails(string empId, Employee employee);
        Task<Employee> GetEmployee(string empId);
        Task<List<Employee>> GetAllEmployeeDetails();
        Task<List<Employee>> GetEmpByJobId(string jobId);
        Task AddJobAsync(Job job);
        Task RemoveJobAsync(string jobid);

    }
}
