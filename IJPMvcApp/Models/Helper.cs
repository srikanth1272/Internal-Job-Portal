using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

namespace IJPMvcApp.Models
{
    public class Helper
    { 
        IHttpContextAccessor accessor = new HttpContextAccessor();
        public string GetToken()
        {
            string token = accessor.HttpContext.Session.GetString("token");
            return token;
        }

        public static async Task<List<SelectListItem>> GetJobs()
        {
            List<SelectListItem> jobIds = new List<SelectListItem>();
            Helper obj = new Helper();
            string token =obj.GetToken();
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/JobSvc/") };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Job> jobs = await client.GetFromJsonAsync<List<Job>>("");
            foreach (Job job in jobs)
            {
                jobIds.Add(new SelectListItem { Text = $"{job.JobId} : {job.JobTitle}", Value = job.JobId  });
            }
            return jobIds;
        }
        public static async Task<List<SelectListItem>> GetSkills()
        {
            List<SelectListItem> skillIds = new List<SelectListItem>();
            Helper obj = new Helper();
            string token = obj.GetToken();
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/SkillSvc/") };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Skill> skills = await client.GetFromJsonAsync<List<Skill>>("");
            foreach (Skill skill in skills)
            {
                skillIds.Add(new SelectListItem { Text = $"{skill.SkillId} : {skill.SkillName}", Value = skill.SkillId });
            }
            return skillIds;
        }
        public static async Task<List<SelectListItem>> GetEmployees()
        {
            List<SelectListItem> employeeIds = new List<SelectListItem>();
            Helper obj = new Helper();
            string token = obj.GetToken();
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/EmployeeSvc/") };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Employee> employees = await client.GetFromJsonAsync<List<Employee>>("");
            foreach (Employee employee in employees)
            {
                employeeIds.Add(new SelectListItem { Text = $"{employee.EmpId} : {employee.EmpName}", Value = employee.EmpId });
            }
            return employeeIds;
        }
        public static async Task<List<SelectListItem>> GetJobposts()
        {
            List<SelectListItem> postIds = new List<SelectListItem>();
            Helper obj = new Helper();
            string token = obj.GetToken();
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/JobPostSvc/") };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<JobPost> JobPosts = await client.GetFromJsonAsync<List<JobPost>>("");
            foreach (JobPost jobPost in JobPosts)
            {
                postIds.Add(new SelectListItem { Text = $"{jobPost.PostId}", Value = $"{jobPost.PostId}" });
            }
            return postIds;
        }
        public static async Task<List<SelectListItem>> GetStatus()
        {
             List<SelectListItem> status = new List<SelectListItem>();

             status.Add(new SelectListItem { Text = "Reviewing", Value = "Reviewing" });
             status.Add(new SelectListItem { Text = "Accepted", Value = "Accepted" });
             status.Add(new SelectListItem { Text = "Rejected", Value = "Rejected" });
             return status;
        }
        public static async Task<List<SelectListItem>> GetLevel()
        {
            List<SelectListItem> level = new List<SelectListItem>();

            level.Add(new SelectListItem { Text = "B", Value = "B" });
            level.Add(new SelectListItem { Text = "I", Value = "I" });
            level.Add(new SelectListItem { Text = "A", Value = "A" });
            return level;
        }
    }
}
