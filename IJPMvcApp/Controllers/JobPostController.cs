using IJPMvcApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IJPMvcApp.Controllers
{
    public class JobPostController : Controller
    {
        HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5117/api/JobPost/") };
        public async Task<ActionResult> Index()
        {
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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobPost jobPost)
        {
            try
            {
                await client.PostAsJsonAsync("", jobPost);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("JobPost/Edit/{postId}")]
        public async Task<ActionResult> Edit(int postId)
        {
            JobPost jobPost = await client.GetFromJsonAsync<JobPost>("" + postId);
            return View(jobPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("JobPost/Edit/{postId}")]
        public async Task<ActionResult> Edit(int postId, JobPost jobPost)
        {
            try
            {
                await client.PutAsJsonAsync(""+postId, jobPost);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("JobPost/Delete/{postId}")]
        public async Task<ActionResult> Delete(int postId)
        {
            JobPost jobPost = await client.GetFromJsonAsync<JobPost>("" + postId);
            return View(jobPost); 
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("JobPost/Delete/{postId}")]
        public async Task<ActionResult> Delete(int postId, JobPost jobPost)
        {
            try
            {  
                await client.DeleteAsync("" + postId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
