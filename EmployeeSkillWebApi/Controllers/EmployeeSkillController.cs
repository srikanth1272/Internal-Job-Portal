using EmployeeSkillLibrary.Models;
using EmployeeSkillLibrary.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkillWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeSkillController : ControllerBase
    {
        IEmployeeSkillRepoAsync employeeSkillRepo;
        public EmployeeSkillController(IEmployeeSkillRepoAsync repo)
        {
            employeeSkillRepo = repo;
        }
        [HttpPost]
        public async Task<ActionResult> Insert(EmployeeSkill employeeSkill)
        {
            try {
                await employeeSkillRepo.AddEmployeeSkillAsync(employeeSkill);
                return Created($"api/EmployeeSkill/{employeeSkill.EmpId}/{employeeSkill.SkillId}", employeeSkill);

            }
            catch(EmployeeSkillException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPost("Employee")]
        public async Task<ActionResult> InsertEmployee(Employee employee)
        {
            try
            {
                await employeeSkillRepo.AddEmployeeAsync(employee);
                return Created();

            }
            catch (EmployeeSkillException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("skill")]
        public async Task<ActionResult> InsertSkill(Skill skill)
        {
            try
            {
                await employeeSkillRepo.AddSkillAsync(skill);
                return Created();

            }
            catch (EmployeeSkillException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<EmployeeSkill> employeeSkills = await employeeSkillRepo.GetAllEmployeeSkillsAsync();
            return Ok(employeeSkills);
        }

        [HttpGet("{empId}/{skillId}")]
        public async Task<ActionResult> GetDetails(string empId, string skillId)
        {
            try
            {
                EmployeeSkill employeeSkill = await employeeSkillRepo.GetEmployeeSkillByEmpIdandSkillIdAsync(empId, skillId);
                return Ok(employeeSkill);
            }
            catch (EmployeeSkillException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ByskillId/{skillId}")]
        public async Task<ActionResult> GetDetailsBySkillId( string skillId)
        {
            try
            {
                List<EmployeeSkill> employeeSkills = await employeeSkillRepo.GetEmployeeSkillsBySkillIdAsync(skillId);
                return Ok(employeeSkills);
            }
            catch (EmployeeSkillException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByempId/{empId}")]
        public async Task<ActionResult> GetDetailsByempId(string empId)
        {
            try
            {
                List<EmployeeSkill> employeeSkills = await employeeSkillRepo.GetEmployeeSkillsByEmpIdAsync(empId);
                return Ok(employeeSkills);
            }
            catch (EmployeeSkillException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{empId}/{skillId}")]
        public async Task<ActionResult> Update(string empId,string skillId, EmployeeSkill employeeSkill)
        {
            try
            {
                await employeeSkillRepo.UpdateEmployeeSkillAsync(empId, skillId,employeeSkill);
                return Ok(employeeSkill);
            }
            catch (EmployeeSkillException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{empId}/{skillId}")]
        public async Task<ActionResult> Delete(string empId, string skillId)
        {
            try
            {
                await employeeSkillRepo.RemoveEmployeeSkillAsync(empId, skillId);
                return Ok();
            }
            catch (EmployeeSkillException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("Employee/{empId}")]
        public async Task<ActionResult> DeleteEmployee(string empId)
        {
            try
            {
                await employeeSkillRepo.RemoveEmployeeAsync(empId);
                return Ok();
            }
            catch (EmployeeSkillException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("Skill/{skillId}")]
        public async Task<ActionResult> DeleteSkill(string skillId)
        {
            try
            {
                await employeeSkillRepo.RemoveSkillAsync(skillId);
                return Ok();
            }
            catch (EmployeeSkillException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
