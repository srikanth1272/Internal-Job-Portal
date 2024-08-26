using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillLibrary;
using SkillLibrary.Models;
using SkillLibrary.Repos;
namespace SkillWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                await repo.RemoveSkillDetailsAsync(skillId);
                return Ok();
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
