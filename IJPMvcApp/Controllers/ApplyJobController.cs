using IJPMvcApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace IJPMvcApp.Controllers
{
    public class ApplyJobController : Controller
    {
        HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5086/api/ApplyJob/") };
        public async Task<ActionResult> Index()
        {
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplyJob appliedJob)
        {
            try
            {
                await client.PostAsJsonAsync("", appliedJob);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("ApplyJob/Edit/{postId}/{empId}")]
        public async Task<ActionResult> Edit(int postId,string empId)
        {
            ApplyJob appliedJob = await client.GetFromJsonAsync<ApplyJob>($"{postId}/{empId}");
            return View(appliedJob);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ApplyJob/Edit/{postId}/{empId}")]
        public async Task<ActionResult> Edit(int postId, string empId, ApplyJob appliedJob)
        {
            try
            {
                await client.PutAsJsonAsync($"{postId}/{empId}", appliedJob);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("ApplyJob/Delete/{postId}/{empId}")]
        public async Task<ActionResult> Delete(int postId, string empId)
        {
            ApplyJob appliedJob = await client.GetFromJsonAsync<ApplyJob>($"{postId}/{empId}");
            return View(appliedJob);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ApplyJob/Delete/{postId}/{empId}")]
        public async Task<ActionResult> Delete(int postId, string empId, ApplyJob appliedJob)
        {
            try
            {
                await client.DeleteAsync($"{postId}/{empId}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
