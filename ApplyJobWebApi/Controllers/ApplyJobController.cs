using ApplyJobLibrary.Models;
using ApplyJobLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplyJobWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplyJobController : ControllerBase
    {
        IApplyJobRepoAsyn repo;
        public ApplyJobController(IApplyJobRepoAsyn repository)
        {
            repo = repository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<ApplyJob> appliedJobs = await repo.GetAllApplyJobsAsync();
            return Ok(appliedJobs);
        }
        [HttpGet("{postId}/{empId}")]
        public async Task<ActionResult> GetOne(int postId, string empId)
        {
            try
            {
                ApplyJob appliedJob = await repo.GetApplyJobAsync(postId, empId);
                return Ok(appliedJob);
            }
            catch (ApplyJobException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByPostId/{postId}")]
        public async Task<ActionResult> GetAllByPostId(int postId)
        {
            try
            {
                List<ApplyJob> appliedJobs = await repo.GetApplyJobsByPostIdAsync(postId);
                return Ok(appliedJobs);
            }
            catch (ApplyJobException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByEmpId/{empId}")]
        public async Task<ActionResult> GetAllByEmpId(string empId)
        {
            try
            {
                List<ApplyJob> appliedJobs = await repo.GetApplyJobsByEmpIdAsync(empId);
                return Ok(appliedJobs);
            }
            catch (ApplyJobException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(ApplyJob appliedJob)
        {
            try
            {
                await repo.AddApplyJobAsync(appliedJob);
                return Created($"api/ApplyJob/{appliedJob.PostId}/{appliedJob.EmpId}", appliedJob);
            }
            catch (ApplyJobException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("JobPost")]
        public async Task<ActionResult> InsertJobPost(JobPost jobPost)
        {
            try
            {
                await repo.AddJobPostAsync(jobPost);
                return Created();
            }
            catch (ApplyJobException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Employee")]
        public async Task<ActionResult> InsertEmployee(Employee employee)
        {
            try
            {
                    await repo.AddEmployeeAsync(employee);
                    return Created();
            }
            catch (ApplyJobException ex)
            {
                    return BadRequest(ex.Message);
            }
        }

        [HttpPut("{postId}/{empId}")]
        public async Task<ActionResult> Update(int postId,string empId, ApplyJob appliedJob)
        {
            try
            {
                await repo.UpdateApplyJobAsync(postId, empId, appliedJob);
                return Ok(appliedJob);
            }
            catch (ApplyJobException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{postId}/{empId}")]
        public async Task<ActionResult> Delete(int postId, string empId)
        {
            try
            {
                await repo.RemoveApplyJobAsync(postId,empId);
                return Ok();
            }
            catch (ApplyJobException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("JobPost/{postId}")]
        public async Task<ActionResult> DeleteJobPost(int postId)
        {
            try
            {
                await repo.RemoveJobPostAsync(postId);
                return Ok();
            }
            catch (ApplyJobException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Employee/{empId}")]
        public async Task<ActionResult> DeleteEmployee(string empId)
        {
            try
            {
                await repo.RemoveEmployeeAsync(empId);
                return Ok();
            }
            catch (ApplyJobException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
