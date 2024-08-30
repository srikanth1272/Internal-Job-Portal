using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobLibrary;
using JobLibrary.Models;
using JobLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace JobWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {
        IJobRepoAsync repo;
        public JobController(IJobRepoAsync repository)
        {
            repo = repository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<Job> jobs = await repo.GetAllJobDetailsAsync();
            return Ok(jobs);
        }

        [HttpGet("{jobId}")]

        public async Task<ActionResult> Details(string jobId)
        {
            try
            {
                Job job = await repo.GetJobAsync(jobId);
                return Ok(job);
            }
            catch (JobException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Job job)
        {
            try
            {
                await repo.AddJobDetailsAsync(job);
                string userName = "Harry";
                string role = "admin";
                string secretKey = "My Name is James, James Bond 007";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/AuthSvc/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
                
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/JobPostSvc/") };
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client.PostAsJsonAsync("Job/", new { JobId = job.JobId});

                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/JobSkillSvc/") };
                client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client2.PostAsJsonAsync("Job/", new { JobId = job.JobId });

                HttpClient client3 = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/EmployeeSvc/") };
                client3.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client3.PostAsJsonAsync("Job/", new { JobId = job.JobId });

                return Created($"api/Job/{job.JobId}", job);
            }
            catch (JobException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{jobId}")]
        public async Task<ActionResult> Edit(string jobId, Job job)
        {
            try
            {
                await repo.UpdateJobDetailsAsync(jobId, job);
                return Ok(job);
            }
            catch (JobException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{jobId}")]
        public async Task<ActionResult> Delete(string jobId)
        {
            string userName = "Harry";
            string role = "admin";
            string secretKey = "My Name is James, James Bond 007";
            HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/AuthSvc/") };
            string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/JobPostSvc/") };

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync("Job/" +jobId);

            HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/JobSkillSvc/") };
            client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response1 = await client2.DeleteAsync("Job/" + jobId);

            HttpClient client3 = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/EmployeeSvc/") };
            client3.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response2 = await client3.DeleteAsync("Job/" + jobId);
            if (response.IsSuccessStatusCode && response1.IsSuccessStatusCode && response2.IsSuccessStatusCode )
            {
                await repo.RemoveJobDetailsAsync(jobId);
                return Ok();
            }
            else
            {
                if (response.IsSuccessStatusCode)
                {
                    HttpClient client4 = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/JobPostSvc/") };
                    client4.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    await client4.PostAsJsonAsync("Job/", new { JobId = jobId }); 
                }
                if (response1.IsSuccessStatusCode)
                {
                    HttpClient client5 = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/JobSkillSvc/") };
                    client5.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    await client5.PostAsJsonAsync("Job/", new { JobId = jobId });
                }
                if (response2.IsSuccessStatusCode)
                {
                    HttpClient client6 = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/EmployeeSvc/") };
                    client6.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    await client6.PostAsJsonAsync("Job/", new { JobId = jobId });
                }
                   
                string errorMessage = string.Empty;
                if (!response.IsSuccessStatusCode)
                {
                    var errContent = await response.Content.ReadAsStringAsync();
                    var errorObj = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(errContent);
                    errorMessage += errorObj.GetProperty("message").GetString()+ "<br/><br/>";
                }
                if (!response1.IsSuccessStatusCode)
                {
                    var errContent = await response1.Content.ReadAsStringAsync();
                    var errorObj = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(errContent);
                    errorMessage += errorObj.GetProperty("message").GetString()+ "<br/><br/>";
                }
                if (!response2.IsSuccessStatusCode)
                {
                    var errContent = await response2.Content.ReadAsStringAsync();
                    var errorObj = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(errContent);
                    errorMessage += errorObj.GetProperty("message").GetString();
                }

                return BadRequest(errorMessage);
            }
        }  
    }
}
