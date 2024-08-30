using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobSkillLibrary.Models;
using JobSkillLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
namespace JobSkillWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobSkillController : ControllerBase
    {
        IJobSkillRepoAsync jobSkillRepoAsync;
        public JobSkillController(IJobSkillRepoAsync repo)
        {
            jobSkillRepoAsync = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<JobSkill> jobskills=await jobSkillRepoAsync.GetAllJobSkillsAsync();
            return Ok(jobskills);
        }

        [HttpGet("{jobId}/{skillId}")]
        public async Task<ActionResult> Get(string jobId, string skillId)
        {
            try
            {
                JobSkill jobskill = await jobSkillRepoAsync.GetJobSkillByJobIdandSkillIdAsync(jobId, skillId);
                return Ok(jobskill);
            }
            catch (JobSkillException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByJobId/{jobId}")]
        public async Task<ActionResult> GetByJobId(string jobId)
        {
            try
            {
                List<JobSkill> jobskills = await jobSkillRepoAsync.GetJobSkillsByJobIdAsync(jobId);
                return Ok(jobskills);
            }
            catch (JobSkillException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{skillId}")]
        public async Task<ActionResult> GetBySkillId(string skillId)
        {
            try
            {
                List<JobSkill> jobskills = await jobSkillRepoAsync.GetJobSkillsBySkillIdAsync(skillId);
                return Ok(jobskills);
            }
            catch (JobSkillException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertJobSkill(JobSkill jobSkill)
        {
            try
            {
                await jobSkillRepoAsync.AddJobSkillAsync(jobSkill);
                return Created($"api/JobSkill/{jobSkill.JobId}/{jobSkill.SkillId}",jobSkill);
            }
            catch (JobSkillException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("Job")]
        public async Task<ActionResult> InsertJob(Job job)
        {
            try
            {
                await jobSkillRepoAsync.AddJobAsync(job);
                return Created();
            }
            catch (JobSkillException ex)
            {
                return BadRequest(new { Message = ex.Message });

            }
        }

        [HttpPost("Skill")]
        public async Task<ActionResult> InsertSkill(Skill skill)
        {
            try
            {
                await jobSkillRepoAsync.AddSkillAsync(skill);
                return Created();
            }
            catch (JobSkillException ex)
            {
                return BadRequest(new { Message = ex.Message });

            }
        }

        [HttpPut("{jobId}/{skillId}")]
        public async Task<ActionResult> Update(string jobId,string skillId,JobSkill jobSkill)
        {
            try
            {
                await jobSkillRepoAsync.UpdateJobSkillAsync(jobId, skillId, jobSkill);
                return Ok(jobSkill);
            }
            catch (JobSkillException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{jobId}/{skillId}")]
        public async Task<ActionResult> Delete(string jobId,string skillId)
        {
            try
            {
                await jobSkillRepoAsync.RemoveJobSkillAsync(jobId,skillId);
                return Ok();
            }
            catch (JobSkillException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Job/{jobId}")]
        public async Task<ActionResult> DeleteJob(string jobId)
        {
            try
            {
                await jobSkillRepoAsync.RemoveJobAsync(jobId);
                return Ok();
            }           
            catch (JobSkillException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("Skill/{skillId}")]
        public async Task<ActionResult> DeleteSkill( string skillId)
        {
            try
            {
                await jobSkillRepoAsync.RemoveSkillAsync(skillId);
                return Ok();
            }
            catch (JobSkillException ex)
            {
                 return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
