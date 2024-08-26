using EmployeeLibrary.Models;
using EmployeeSkillLibrary.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLibrary.Repo
{
    public class EFEmployeeRepoAsync : IEmployeeRepoAsync
    {
        EmployeeDBContext ctx=new EmployeeDBContext();
        public async Task AddEmployeeDetails(Employee employee)
        {
            try
            {
                await ctx.Employees.AddAsync(employee);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task AddJobAsync(Job job)
        {
            try
            {
                await ctx.Jobs.AddAsync(job);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task<List<Employee>> GetAllEmployeeDetails()
        {
            List<Employee> employees = await ctx.Employees.ToListAsync();
            return employees;
        }

        public async Task<List<Employee>> GetEmpByJobId(string jobId)
        {
            List<Employee> employees = await (from e in ctx.Employees where e.JobId == jobId select e).ToListAsync();
            if (employees.Count > 0)
                return employees;
            else
                throw new EmployeeException("No employee with this JobId");
        }

        public async Task<Employee> GetEmployee(string empId)
        {
            try
            {
                Employee employee = await (from e in ctx.Employees where e.EmpId == empId select e).FirstAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task RemoveEmployeeDetails(string empId)
        {
            try
            {
                Employee employee = await GetEmployee(empId);
                ctx.Employees.Remove(employee);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task RemoveJobAsync(string jobId)
        {
            try
            {
                Job job = await (from e in ctx.Jobs where e.JobId==jobId select e).FirstAsync();
                ctx.Jobs.Remove(job);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task UpdateEmployeeDetails(string empId, Employee employee)
        {
            try
            {
                Employee employee1 = await GetEmployee(empId);
                employee1.EmpName = employee.EmpName;
                employee1.PhoneNo = employee.PhoneNo;
                employee1.EmailId = employee.EmailId;
                employee1.TotalExperience = employee.TotalExperience;
                employee1.JobId = employee.JobId;
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }
    }
}
