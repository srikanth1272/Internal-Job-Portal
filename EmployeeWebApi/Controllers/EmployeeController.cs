﻿using EmployeeLibrary.Models;
using EmployeeSkillLibrary.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            List<Employee> employees=await employeeRepo.GetAllEmployeeDetailsAsync();
            return Ok(employees);
        }

        [HttpGet("ByempId/{empId}")]
        public async Task<ActionResult> GetByEmpId(string empId)
        {
            try
            {
                Employee employee = await employeeRepo.GetEmployeeAsync(empId);
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
                List<Employee> employees = await employeeRepo.GetEmpByJobIdAsync(jobId);
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
                string userName = "Harry";
                string role = "admin";
                string secretKey = "My Name is James, James Bond 007";
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5059/api/Auth/") };
                string token = await client2.GetStringAsync($"{userName}/{role}/{secretKey}");

                await employeeRepo.AddEmployeeDetailsAsync(employee);
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5086/api/ApplyJob/") };
                await client.PostAsJsonAsync("Employee/", new { EmpId = employee.EmpId });
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5064/api/EmployeeSkill/") };
                client1.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client1.PostAsJsonAsync("Employee/", new { EmpId = employee.EmpId });

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
                await employeeRepo.UpdateEmployeeDetailsAsync(empId, employee);
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
                string userName = "Harry";
                string role = "admin";
                string secretKey = "My Name is James, James Bond 007";
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5059/api/Auth/") };
                string token = await client2.GetStringAsync($"{userName}/{role}/{secretKey}");


                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5086/api/ApplyJob/") };
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.DeleteAsync("Employee/" +empId);
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5064/api/EmployeeSkill/") };
                client1.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response1 = await client1.DeleteAsync("Employee/" + empId);
                if (response.IsSuccessStatusCode && response1.IsSuccessStatusCode)
                {
                    await employeeRepo.RemoveEmployeeDetailsAsync(empId);
                    return Ok();
                }
                else
                {
                    if (response.IsSuccessStatusCode)
                    {
                        HttpClient client4 = new HttpClient() { BaseAddress = new Uri("http://localhost:5086/api/ApplyJob/") };
                        client4.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        await client4.PostAsJsonAsync("Employee/", new { EmpId = empId });

                    }
                    if (response1.IsSuccessStatusCode)
                    {
                        HttpClient client5 = new HttpClient() { BaseAddress = new Uri("http://localhost:5064/api/EmployeeSkill/") };
                        client5.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        await client5.PostAsJsonAsync("Employee/", new { EmpId = empId });
                    }
                    return BadRequest("Cannot delete the Employee");
                }
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
