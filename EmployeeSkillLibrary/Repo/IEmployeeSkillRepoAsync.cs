using EmployeeSkillLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillLibrary.Repo
{
    public interface IEmployeeSkillRepoAsync
    {
        Task AddEmployeeSkillAsync(EmployeeSkill employeeskill);
        Task RemoveEmployeeSkillAsync(string empId, string skillId);
        Task UpdateEmployeeSkillAsync(string empId, string skillId, EmployeeSkill employeeSkill);
        Task<EmployeeSkill> GetEmployeeSkillByEmpIdandSkillIdAsync(string empId, string skillId);
        Task<List<EmployeeSkill>> GetAllEmployeeSkillsAsync();
        Task<List<EmployeeSkill>> GetEmployeeSkillsByEmpIdAsync(string empId);
        Task<List<EmployeeSkill>> GetEmployeeSkillsBySkillIdAsync(string skillId);
        Task AddEmployeeAsync(Employee employee);
        Task AddSkillAsync(Skill skill);
        Task RemoveSkillAsync(string skillId);
        Task RemoveEmployeeAsync(string empId);

    }
}
