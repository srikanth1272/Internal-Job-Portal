using EmployeeSkillLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillLibrary.Repo
{
    public class EFEmployeeSkillRepoAsync : IEmployeeSkillRepoAsync
    {
        EmployeeSkillDBContext ctx=new EmployeeSkillDBContext();
        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                await ctx.Employees.AddAsync(employee);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeSkillException(ex.Message);
            }
        }

        public async Task AddEmployeeSkillAsync(EmployeeSkill employeeskill)
        {
            try
            {
                await ctx.EmployeeSkills.AddAsync(employeeskill);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeSkillException(ex.InnerException.Message);
            }
        }

        public async Task AddSkillAsync(Skill skill)
        {
            try
            {
                await ctx.Skills.AddAsync(skill);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeSkillException(ex.Message);
            }
        }

        public async Task<List<EmployeeSkill>> GetAllEmployeeSkillsAsync()
        {
            List<EmployeeSkill> employeeSkills = await ctx.EmployeeSkills.ToListAsync();
            return employeeSkills;
        }

        public async Task<EmployeeSkill> GetEmployeeSkillByEmpIdandSkillIdAsync(string empId, string skillId)
        {
            try
            {
                EmployeeSkill employeeSkill = await (from es in ctx.EmployeeSkills where es.EmpId == empId && es.SkillId == skillId select es).FirstAsync();
                return employeeSkill;
            }
            catch (Exception ex)
            {
                throw new EmployeeSkillException(ex.Message);
            }
        }

        public async Task<List<EmployeeSkill>> GetEmployeeSkillsByEmpIdAsync(string empId)
        {
                List<EmployeeSkill> employeeSkills = await (from es in ctx.EmployeeSkills where es.EmpId == empId select es).ToListAsync();
                if (employeeSkills.Count > 0)
                    return employeeSkills;
                else
                    throw new EmployeeSkillException("There are no Skills for this Employee Id");
        }

        public async Task<List<EmployeeSkill>> GetEmployeeSkillsBySkillIdAsync(string skillId)
        {
            List<EmployeeSkill> employeeSkills = await (from es in ctx.EmployeeSkills where es.SkillId == skillId select es).ToListAsync();
            if (employeeSkills.Count > 0)
                return employeeSkills;
            else
                throw new EmployeeSkillException("There are no employess for this Skill Id");

        }

        public async Task RemoveEmployeeAsync(string empId)
        {
            try
            {
                Employee employee = await (from e in ctx.Employees where e.EmpId == empId select e).FirstAsync();
                ctx.Employees.Remove(employee);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeSkillException(ex.InnerException.Message);
            }
        }

        public async Task RemoveEmployeeSkillAsync(string empId, string skillId)
        {
            try
            {
                EmployeeSkill employeeSkill = await GetEmployeeSkillByEmpIdandSkillIdAsync(empId, skillId);
                ctx.EmployeeSkills.Remove(employeeSkill);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)    
            {
                throw new EmployeeSkillException(ex.Message);
            }
        }

        public async Task RemoveSkillAsync(string skillId)
        {
            try
            {
                Skill skill = await (from s in ctx.Skills where s.SkillId == skillId select s).FirstAsync();
                ctx.Skills.Remove(skill);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeSkillException(ex.InnerException.Message);
            }
        }

        public async Task UpdateEmployeeSkillAsync(string empId, string skillId, EmployeeSkill employeeSkill)
        {
            try
            {
                EmployeeSkill employeeSkill1 = await GetEmployeeSkillByEmpIdandSkillIdAsync(empId, skillId);
                employeeSkill1.EmpId = employeeSkill.EmpId;
                employeeSkill1.SkillId=employeeSkill.SkillId;
                employeeSkill1.SkillExperience = employeeSkill.SkillExperience;
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeSkillException(ex.Message);
            }
        }
    }
}
