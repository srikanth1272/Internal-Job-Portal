
using IJPMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IJPMvcApp.Controllers
{
    [Authorize]
    public class ApplyJobController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/ApplyJobSvc/") };
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<ApplyJob> appliedJobs = await client.GetFromJsonAsync<List<ApplyJob>>("");
            return View(appliedJobs);
        }

        public async Task<ActionResult> Details(int postid, string empId)
        {
            ApplyJob appliedJob = await client.GetFromJsonAsync<ApplyJob>($"{postid}/{empId}");
            return View(appliedJob);
        }
        public async Task<ActionResult> ByPostId(int postId)
        {
            List<ApplyJob> appliedJobs = await client.GetFromJsonAsync<List<ApplyJob>>("ByPostId/" + postId);
            return View(appliedJobs);
        }
        public async Task<ActionResult> ByEmpId(string empId)
        {
            List<ApplyJob> appliedJobs = await client.GetFromJsonAsync<List<ApplyJob>>("ByEmpId/" + empId);
            return View(appliedJobs);
        }
        public ActionResult Create()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            ApplyJob appliedJob = new ApplyJob();
            appliedJob.ApplicationStatus = "Reviewing";
            appliedJob.AppliedDate = DateOnly.FromDateTime(DateTime.Now);
            return View(appliedJob);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplyJob appliedJob)
        {
          
            var response = await client.PostAsJsonAsync("", appliedJob);
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

        [Route("ApplyJob/Edit/{postId}/{empId}")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Edit(int postId,string empId)
        {
            ApplyJob appliedJob = await client.GetFromJsonAsync<ApplyJob>($"{postId}/{empId}");
            return View(appliedJob);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ApplyJob/Edit/{postId}/{empId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int postId, string empId, ApplyJob appliedJob)
        {
            await client.PutAsJsonAsync($"{postId}/{empId}", appliedJob);
            return RedirectToAction(nameof(Index));
        }

        [Route("ApplyJob/Delete/{postId}/{empId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int postId, string empId)
        {
            ApplyJob appliedJob = await client.GetFromJsonAsync<ApplyJob>($"{postId}/{empId}");
            return View(appliedJob);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ApplyJob/Delete/{postId}/{empId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int postId, string empId, ApplyJob appliedJob)
        {
                var response=await client.DeleteAsync($"{postId}/{empId}");
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
        }
    }
}
