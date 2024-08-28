using Microsoft.AspNetCore.Mvc.Rendering;

namespace IJPMvcApp.Models
{
    public class Helper
    {
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
