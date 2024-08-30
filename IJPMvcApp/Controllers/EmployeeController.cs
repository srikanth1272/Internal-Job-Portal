using IJPMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IJPMvcApp.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: EmployeeController
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/EmployeeSvc/") };
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Employee> employees = await client.GetFromJsonAsync<List<Employee>>("");
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(string empId)
        {
            Employee employee = await client.GetFromJsonAsync<Employee>("ByempId/" + empId); 
            return View(employee);
        }

        public async Task<ActionResult> ByjobId(string jobId)
        {
            List<Employee> employees = await client.GetFromJsonAsync<List<Employee>>("ByjobId/" + jobId);
            return View(employees);
        }

        // GET: EmployeeController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Employee employee)
        {
                var response=await client.PostAsJsonAsync("", employee);
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

        // GET: EmployeeController/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string empId)
        {
            Employee employee = await client.GetFromJsonAsync<Employee>("ByempId/" + empId);
            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string empId, Employee employee)
        {
             await client.PutAsJsonAsync(empId, employee);
             return RedirectToAction(nameof(Index));
        }

        // GET: EmployeeController/Delete/5
        [Route("Employee/Delete/{empId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string empId)
        {
            Employee employee = await client.GetFromJsonAsync<Employee>("ByempId/" + empId);
            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Employee/Delete/{empId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string empId, IFormCollection collection)
        {
            var response = await client.DeleteAsync("" + empId);
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
