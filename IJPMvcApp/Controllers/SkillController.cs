using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IJPMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
namespace IJPMvcApp.Controllers
{
    [Authorize]
    public class SkillController : Controller
    {
        // GET: SkillController
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/SkillSvc/") };

        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Skill> skills = await client.GetFromJsonAsync<List<Skill>>("");
            return View(skills);
        }

        // GET: SkillController/Details/5
        public async Task<ActionResult> Details(string skillId)
        {
            Skill skill = await client.GetFromJsonAsync<Skill>("" + skillId);
            return View(skill);
        }

        // GET: SkillController/Create
       [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: SkillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async  Task<ActionResult> Create(Skill skill)
        {
            
            var response=await client.PostAsJsonAsync<Skill>("", skill);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorObj = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(errorContent);
                string errorMessage = errorObj.GetProperty("message").GetString();

                throw new Exception(errorMessage);
            }
        }

        // GET: SkillController/Edit/5
        [Route("Skill/Edit/{skillId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string skillId)
        {
            Skill skill = await client.GetFromJsonAsync<Skill>("" + skillId);
            return View(skill);
        }

        // POST: SkillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Skill/Edit/{skillId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string skillId, Skill skill)
        {
                await client.PutAsJsonAsync<Skill>($"{skillId}", skill);
                return RedirectToAction(nameof(Index));
        }

        // GET: SkillController/Delete/5
        
        [Authorize(Roles = "Admin")]
        [Route("Skill/Delete/{skillId}")]

        public async Task<ActionResult> Delete(string skillId)
        {
            Skill skill=await client.GetFromJsonAsync<Skill>(""+skillId);
            return View(skill);
        }

        // POST: SkillController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Route("Skill/Delete/{skillId}")]

        public async Task<ActionResult> Delete(string skillId, IFormCollection collection)
        {
            
            var response = await client.DeleteAsync($"{skillId}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                throw new Exception(errorContent);
            }

        }
        public async Task<ActionResult> GetBySkillLevel(string skillLevel)
        {
            List<Skill> skills = await client.GetFromJsonAsync<List<Skill>>("BySkillLevel/"+skillLevel);
            return View(skills);
        }
        public async Task<ActionResult> GetBySkillCategory(string skillCategory)
        {
            List<Skill> skills = await client.GetFromJsonAsync<List<Skill>>("BySkillCategory/"+skillCategory);
            return View(skills);
        }
    }
}
