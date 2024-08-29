using IJPMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IJPMvcApp.Controllers
{
    [Authorize]
    public class JobSkillController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/JobSkillSvc/") };
        // GET: JobSkillController
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<JobSkill> jobskills = await client.GetFromJsonAsync<List<JobSkill>>("");
            return View(jobskills);
        }

        // GET: JobSkillController/Details/5
        public async Task<ActionResult> Details(string jobId, string skillId)
        {
            JobSkill jobskill = await client.GetFromJsonAsync<JobSkill>("" + jobId + "/" + skillId);
            return View(jobskill);
        }
        public async Task<ActionResult> DetailsByJobId(string jobId)
        {
            List<JobSkill> jobskills = await client.GetFromJsonAsync<List<JobSkill>>("GetByJobId/" + jobId);
            return View(jobskills);
        }
        public async Task<ActionResult> DetailsBySkillId(string skillId)
        {
            List<JobSkill> jobskills = await client.GetFromJsonAsync<List<JobSkill>>("" + skillId);
            return View(jobskills);
        }

        // GET: JobSkillController/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: JobSkillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Create(JobSkill jobSkill)
        {
            try
            {
                var response=await client.PostAsJsonAsync("", jobSkill);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex) { throw; }
        }


        // GET: JobSkillController/Edit/5
        [Route("JobSkill/Edit/{jobId}/{skillId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string jobId, string skillId)
        {
            JobSkill jobskill = await client.GetFromJsonAsync<JobSkill>("" + jobId + "/" + skillId);
            return View(jobskill);
        }

        // POST: JobSkillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("JobSkill/Edit/{jobId}/{skillId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string jobId,string skillId, JobSkill jobSkill)
        {
            try
            {
                await client.PutAsJsonAsync(""+jobId+"/"+skillId, jobSkill);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: JobSkillController/Delete/5
        [Authorize(Roles = "Admin")]
        [Route("JobSkill/Delete/{jobId}/{skillId}")]
        public async Task<ActionResult> Delete(string jobId, string skillId)
        {
            JobSkill jobskill = await client.GetFromJsonAsync<JobSkill>("" + jobId + "/" + skillId);
            return View(jobskill);
        }

        // POST: JobSkillController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Route("JobSkill/Delete/{jobId}/{skillId}")]
        public async Task<ActionResult> Delete(string jobId, string skillId, IFormCollection collection)
        {
            try
            {
                var response=await client.DeleteAsync("" + jobId + "/" + skillId);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex) { throw; }
        }
    }
 }
