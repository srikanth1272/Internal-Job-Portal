using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillLibrary;
using SkillLibrary.Models;
using SkillLibrary.Repos;
namespace SkillWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SkillController : ControllerBase
    {
        ISkillRepoAsync repo;
        public SkillController(ISkillRepoAsync repository)
        {
            repo = repository;

        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<Skill> skills = await repo.GetAllSkillDetailsAsync();
            return Ok(skills);
        }
        [HttpGet("{skillId}")]
        public async Task<ActionResult> Details(string skillId)
        {
            try
            {
                Skill skill = await repo.GetSkillAsync(skillId);
                return Ok(skill);
            }
            catch (SkillException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Skill skill)
        {
            try
            {
                await repo.AddSkillDetailsAsync(skill);
                string userName = "Harry";
                string role = "admin";
                string secretKey = "My Name is James, James Bond 007";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5059/api/Auth/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");

                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5064/api/EmployeeSkill/") };
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client.PostAsJsonAsync("Skill/", new { SkillId = skill.SkillId });
               
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5210/api/JobSkill/") };
                client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client2.PostAsJsonAsync("Skill/", new { SkillId = skill.SkillId });
                return Created($"api/Skill/{skill.SkillId}", skill);
            }
            catch (SkillException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{skillId}")]
        public async Task<ActionResult> Edit(string skillId, Skill skill)
        {
            try
            {

                await repo.UpdateSkillDetailsAsync(skillId, skill);
                    return Ok(skill);
                



            }
            catch (SkillException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{skillId}")]
        public async Task<ActionResult> Delete(string skillId)
        {
            try
            {
                
                string userName = "Harry";
                string role = "admin";
                string secretKey = "My Name is James, James Bond 007";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5059/api/Auth/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");


                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5064/api/EmployeeSkill/") };
                client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response1 = await client2.DeleteAsync("Skill/" + skillId);

                HttpClient client3 = new HttpClient() { BaseAddress = new Uri("http://localhost:5210/api/JobSkill/") };
                client3.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response2 = await client3.DeleteAsync("Skill/" + skillId);
                if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode)
                {
                    await repo.RemoveSkillDetailsAsync(skillId);
                    return Ok();
                }
                else
                {
                    return BadRequest("Cannot delete the job");
                }

            }

            catch (SkillException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("BySkillLevel/{skillLevel}")]
        public async Task<ActionResult> GetSkillsBySkillLevel(string skillLevel)
        {
            try
            {
                List<Skill> skills = await repo.GetSkillsByLevelAsync(skillLevel);
                return Ok(skills);
            }
            catch (SkillException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("BySkillCategory/{skillCategory}")]
        public async Task<ActionResult> GetSkillsByCategory(string skillCategory)
        {
            try
            {
                List<Skill> skills = await repo.GetSkillsByCategoryAsync(skillCategory);
                return Ok(skills);
            }
            catch (SkillException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
