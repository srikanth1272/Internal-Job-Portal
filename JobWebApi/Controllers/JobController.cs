using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobLibrary;
using JobLibrary.Models;
using JobLibrary.Repos;

namespace JobWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5117/api/JobPost/") };
                await client.PostAsJsonAsync("Job/", new { JobId = job.JobId});
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5210/api/JobSkill/") };
                await client2.PostAsJsonAsync("Job/", new { JobId = job.JobId });
                HttpClient client3 = new HttpClient() { BaseAddress = new Uri("http://localhost:5005/api/Employee/") };
                await client3.PostAsJsonAsync("Job/", new { JobId = job.JobId });

                
                return Created($"api/Job/{job.JobId}", job);
            }
            catch (JobException ex)
            {
                return BadRequest(ex.Message);
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
            try
            {
                await repo.RemoveJobDetailsAsync(jobId);
                return Ok();
            }
            catch(JobException ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
