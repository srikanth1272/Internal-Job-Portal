using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillLibrary.Models;
namespace SkillLibrary.Repos
{
    public class EFSkillRepoAsync : ISkillRepoAsync
    {
        SkillDBContext ctx=new SkillDBContext();
        public async Task AddSkillDetailsAsync(Skill skill)
        {
            try
            {
                await ctx.Skills.AddAsync(skill);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new SkillException(ex.InnerException.Message);
            }
        }

        public async Task<List<Skill>> GetAllSkillDetailsAsync()
        {
            List<Skill> skills=await ctx.Skills.ToListAsync();
            return skills;
        }

        public async Task<Skill> GetSkillAsync(string skillId)
        {
            try
            {
                Skill skill = await (from e in ctx.Skills where e.SkillId == skillId select e).FirstAsync();
                return skill;
            }
            catch (Exception ex)
            {
                throw new SkillException("No Such Skill Found");
            }
        }

        public async Task<List<Skill>> GetSkillsByCategoryAsync(string skillCategory)
        {
           
            List<Skill> skills = await (from e in ctx.Skills where e.SkillCategory == skillCategory  select e).ToListAsync();
            if (skills.Count > 0)
            {
                return skills;
            }
            else
            {
                throw new SkillException("No Skill Found for that SkillCategory");
            }

        }

        public async Task<List<Skill>> GetSkillsByLevelAsync(string skillLevel)
        {
            List<Skill> skills = await(from e in ctx.Skills where e.SkillLevel == skillLevel select e).ToListAsync();
            if (skills.Count > 0)
            {
                return skills;
            }
            else
            {
                throw new SkillException("No Skill Found for that SkillLevel");
            }
        }

        public async Task RemoveSkillDetailsAsync(string skillId)
        {
            Skill skill=await GetSkillAsync(skillId);
            try
            {
                ctx.Skills.Remove(skill);
                await ctx.SaveChangesAsync();

            }
            catch (Exception ex) { 
                throw new SkillException(ex.Message);
            }
        }

        public async Task UpdateSkillDetailsAsync(string skillId, Skill skill)
        {
            try
            {
                Skill s = await GetSkillAsync(skillId);
                s.SkillName = skill.SkillName;
                s.SkillLevel = skill.SkillLevel;
                s.SkillCategory = skill.SkillCategory;
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new SkillException(ex.Message);
            }
        }
    }
}
