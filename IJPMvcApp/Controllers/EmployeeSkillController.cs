using IJPMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IJPMvcApp.Controllers
{
    [Authorize]
    public class EmployeeSkillController : Controller
    {
        // GET: EmployeeSkillController
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/EmployeeSkillSvc/") };
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<EmployeeSkill> employeeSkills = await client.GetFromJsonAsync<List<EmployeeSkill>>("");
            return View(employeeSkills);
        }

        // GET: EmployeeSkillController/Details/5
        public async Task<ActionResult> Details(string empId, string skillId)
        {
            EmployeeSkill employeeSkill = await client.GetFromJsonAsync<EmployeeSkill>("" + empId + "/" + skillId);
            return View(employeeSkill);
        }
        public async Task<ActionResult> ByempId(string empId)
        {
            List<EmployeeSkill> employeeSkills = await client.GetFromJsonAsync<List<EmployeeSkill>>("ByempId/" + empId);
            return View(employeeSkills);
        }
        public async Task<ActionResult> ByskillId(string skillId)
        {
            List<EmployeeSkill> employeeSkills = await client.GetFromJsonAsync<List<EmployeeSkill>>("ByskillId/"+ skillId);
            return View(employeeSkills);
        }

        // GET: EmployeeSkillController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeSkillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeSkill employeeSkill)
        {
            var response=await client.PostAsJsonAsync("",employeeSkill);
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

        // GET: EmployeeSkillController/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string empId, string skillId)
        {
            EmployeeSkill employeeSkill = await client.GetFromJsonAsync<EmployeeSkill>("" + empId + "/" + skillId);
            return View(employeeSkill);
        }

        // POST: EmployeeSkillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string empId, string skillId, EmployeeSkill employeeSkill)
        {
                await client.PutAsJsonAsync(""+empId +"/"+ skillId,employeeSkill);
                return RedirectToAction(nameof(Index));
        }

        [Route("EmployeeSkill/Delete/{empId}/{skillId}")]
        [Authorize(Roles = "Admin")]
        // GET: EmployeeSkillController/Delete/5
        public async Task<ActionResult> Delete(string empId, string skillId)
        {
            EmployeeSkill employeeSkill = await client.GetFromJsonAsync<EmployeeSkill>("" + empId + "/" + skillId);
            return View(employeeSkill);
        }

        // POST: EmployeeSkillController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Route("EmployeeSkill/Delete/{empId}/{skillId}")]
        public async Task<ActionResult> Delete(string empId, string skillId, EmployeeSkill employeeSkill)
        {
                var response=await client.DeleteAsync(""+empId +"/"+ skillId);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
        }
    }
}
