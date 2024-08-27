using JobPostLibrary.Models;
using JobPostLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPostWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   //[Authorize]
    public class JobPostController : ControllerBase
    {
        IJobPostRepoAsync repo;
        public JobPostController(IJobPostRepoAsync repository)
        {
            repo = repository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<JobPost> jobPosts = await repo.GetAllJobPostsAsync();
            return Ok(jobPosts);
        }
        [HttpGet("{postId}")]
        public async Task<ActionResult> GetOne(int postId)
        {
            try
            {
                JobPost jobPost = await repo.GetJobPostAsync(postId);
                return Ok(jobPost);
            }
            catch (JobPostException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByJobId/{jobId}")]
        public async Task<ActionResult> GetAllByJobId(string jobId)
        {
            try
            {
                List<JobPost> jobPosts = await repo.GetJobPostsByJobIdAsync(jobId);
                return Ok(jobPosts);
            }
            catch (JobPostException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(JobPost jobPost)
        {
            try
            {
                await repo.AddJobPostAsync(jobPost);
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5086/api/ApplyJob/") };
                await client.PostAsJsonAsync("JobPost/",new {PostId = jobPost.PostId});
                return Created($"api/JobPost/{jobPost.PostId}", jobPost);
            }
            catch (JobPostException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Job")]
        public async Task<ActionResult> InsertJob(Job job)
        {
            try
            {
                await repo.AddJobAsync(job);
                return Created();
            }
            catch (JobPostException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{postId}")]
        public async Task<ActionResult> Update(int postId, JobPost jobPost)
        {
            try
            {
                await repo.UpdateJobPostAsync(postId, jobPost);
                return Ok(jobPost);
            }
            catch (JobPostException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{postId}")]
        public async Task<ActionResult> Delete(int postId)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5086/api/ApplyJob/") };
                var response =  await client.DeleteAsync("JobPost/" + postId);
                if (response.IsSuccessStatusCode)
                {
                    await repo.RemoveJobPostAsync(postId);
                    return Ok();
                }
                else
                {
                    return BadRequest("Cannot delete the job");
                }
            }
            catch (JobPostException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("job/{jobId}")]
        public async Task<ActionResult> DeleteJob(string jobId)
        {
            try
            {
               
                await repo.RemoveJobAsync(jobId);
                return Ok();
            }
            catch (JobPostException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
