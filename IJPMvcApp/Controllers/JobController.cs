using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IJPMvcApp.Models;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
namespace IJPMvcApp.Controllers
{
   [Authorize]
    public class JobController : Controller
    {
        // GET: JobController
        static HttpClient client =new HttpClient() { BaseAddress = new Uri("http://localhost:5152/api/Job/") };
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Job> jobs = await client.GetFromJsonAsync<List<Job>>("");
            return View(jobs);
        }

        // GET: JobController/Details/5
        public async Task<ActionResult> Details(string jobId)
        {
           
                Job job = await client.GetFromJsonAsync<Job>(""+jobId);
                return View(job);
            
           
        }

        // GET: JobController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async  Task<ActionResult> Create(Job job)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await client.PostAsJsonAsync<Job>("",job);
            return RedirectToAction(nameof(Index));
            
            
        }

        // GET: JobController/Edit/5
        [Route("Job/Edit/{jobId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string jobId)
        {
            Job job = await client.GetFromJsonAsync<Job>("" + jobId);
            return View(job);
        }

        // POST: JobController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Job/Edit/{jobId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string jobId, Job job)
        {
            try
            {
                await client.PutAsJsonAsync<Job>($"{jobId}",job);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: JobController/Delete/5
        [Authorize(Roles = "Admin")]
        [Route("Job/Delete/{jobId}")]

        public async Task<ActionResult> Delete(string jobId)
        {
            Job job = await client.GetFromJsonAsync<Job>("" + jobId);
            return View(job);
        }

        // POST: JobController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        
         [Authorize(Roles = "Admin")]
        [Route("Job/Delete/{jobId}")]

        public async Task<ActionResult> Delete(string jobId, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync($"{jobId}");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
