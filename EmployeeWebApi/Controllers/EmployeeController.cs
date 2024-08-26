using EmployeeLibrary.Models;
using EmployeeSkillLibrary.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepoAsync employeeRepo;
        public EmployeeController(IEmployeeRepoAsync Repo)
        {
            employeeRepo = Repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Employee> employees=await employeeRepo.GetAllEmployeeDetails();
            return Ok(employees);
        }

        [HttpGet("ByempId/{empId}")]
        public async Task<ActionResult> GetByEmpId(string empId)
        {
            try
            {
                Employee employee = await employeeRepo.GetEmployee(empId);
                return Ok(employee);
            }
            catch (EmployeeException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByjobId/{jobId}")]
        public async Task<ActionResult> GetByjobId(string jobId)
        {
            try
            {
                List<Employee> employees = await employeeRepo.GetEmpByJobId(jobId);
                return Ok(employees);
            }
            catch (EmployeeException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(Employee employee)
        {
            try
            {
                await employeeRepo.AddEmployeeDetails(employee);
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5086/api/ApplyJob/") };
                await client.PostAsJsonAsync("Employee/", new { EmpId = employee.EmpId });
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5064/api/EmployeeSkill/") };
                await client.PostAsJsonAsync("Employee/", new { EmpId = employee.EmpId });

                return Created($"api/Employee/{employee.EmpId}", employee);
            }
            catch (EmployeeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Job")]
        public async Task<ActionResult> InsertJob(Job job)
        {
            try
            {
                await employeeRepo.AddJobAsync(job);
                return Created();
            }
            catch (EmployeeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{empId}")]
        public async Task<ActionResult> Update(string empId, Employee employee)
        {
            try
            {
                await employeeRepo.UpdateEmployeeDetails(empId, employee);
                return Ok(employee);
            }
            catch (EmployeeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{empId}")]
        public async Task<ActionResult> Delete(string empId)
        {
            try
            {
                await employeeRepo.RemoveEmployeeDetails(empId);
                return Ok();
            }
            catch (EmployeeException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("Job/{jobId}")]
        public async Task<ActionResult> DeleteJob(string jobId)
        {
            try
            {
                await employeeRepo.RemoveJobAsync(jobId);
                return Ok();
            }
            catch (EmployeeException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
