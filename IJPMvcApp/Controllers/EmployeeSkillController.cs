using IJPMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IJPMvcApp.Controllers
{
    
    public class EmployeeSkillController : Controller
    {
        // GET: EmployeeSkillController
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5064/api/EmployeeSkill/") };
        public async Task<ActionResult> Index()
        {
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
            try
            {
                await client.PostAsJsonAsync("",employeeSkill);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeSkillController/Edit/5
        public async Task<ActionResult> Edit(string empId, string skillId)
        {
            EmployeeSkill employeeSkill = await client.GetFromJsonAsync<EmployeeSkill>("" + empId + "/" + skillId);
            return View(employeeSkill);
        }

        // POST: EmployeeSkillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string empId, string skillId, EmployeeSkill employeeSkill)
        {
            try
            {
                await client.PutAsJsonAsync(""+empId +"/"+ skillId,employeeSkill);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("EmployeeSkill/Delete/{empId}/{skillId}")]
        // GET: EmployeeSkillController/Delete/5
        public async Task<ActionResult> Delete(string empId, string skillId)
        {
            EmployeeSkill employeeSkill = await client.GetFromJsonAsync<EmployeeSkill>("" + empId + "/" + skillId);
            return View(employeeSkill);
        }

        // POST: EmployeeSkillController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EmployeeSkill/Delete/{empId}/{skillId}")]
        public async Task<ActionResult> Delete(string empId, string skillId, EmployeeSkill employeeSkill)
        {
            try
            {
                await client.DeleteAsync(""+empId +"/"+ skillId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
