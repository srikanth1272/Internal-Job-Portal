using IJPMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IJPMvcApp.Controllers
{
    [Authorize]
    public class JobPostController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/JobPostSvc/") };
        static HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/ApplyJobSvc/") };
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            client2.DefaultRequestHeaders.Authorization = new
            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<JobPost> jobPosts = await client.GetFromJsonAsync<List<JobPost>>("");
            return View(jobPosts);
        }

        public async Task<ActionResult> Details(int postid)
        {
            JobPost jobPost = await client.GetFromJsonAsync<JobPost>(""+postid);
            return View(jobPost);
        }
        public async Task<ActionResult> ByJobId(string jobId)
        {
            List<JobPost> jobPosts = await client.GetFromJsonAsync<List<JobPost>>("ByJobId/"+jobId);
            return View(jobPosts);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(JobPost jobPost)
        {
            
         var response=await client.PostAsJsonAsync("", jobPost);
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
        public ActionResult ApplyJob(int postId)
        {
            ApplyJob applyJob = new ApplyJob();
            applyJob.PostId = postId;
            applyJob.AppliedDate = DateOnly.FromDateTime(DateTime.Now);
            applyJob.ApplicationStatus = "Reviewing";
            return View(applyJob);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> ApplyJob(ApplyJob applyJob)
        {
            var response = await client2.PostAsJsonAsync("", applyJob);
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

        [Route("JobPost/Edit/{postId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int postId)
        {
            JobPost jobPost = await client.GetFromJsonAsync<JobPost>("" + postId);
            return View(jobPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("JobPost/Edit/{postId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int postId, JobPost jobPost)
        {
                await client.PutAsJsonAsync(""+postId, jobPost);
                return RedirectToAction(nameof(Index));
        }

        [Route("JobPost/Delete/{postId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int postId)
        {
            JobPost jobPost = await client.GetFromJsonAsync<JobPost>("" + postId);
            return View(jobPost); 
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("JobPost/Delete/{postId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int postId, JobPost jobPost)
        {
            var response = await client.DeleteAsync("" + postId);
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
    }
}
