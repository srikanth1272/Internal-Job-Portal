using JobSkillLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSkillLibrary.Repos
{
    public class EFJobSkillRepoAsync : IJobSkillRepoAsync
    {
        JobSkillDBContext ctx=new JobSkillDBContext();
        public async Task AddJobAsync(Job job)
        {
            try
            {
                await ctx.Jobs.AddAsync(job);
               await  ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new JobSkillException(ex.InnerException.Message);
            }
        }

        public async Task AddJobSkillAsync(JobSkill jobSkill)
        {
            try
            {
                await ctx.JobSkills.AddAsync(jobSkill);
               await  ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new JobSkillException(ex.InnerException.Message);
            }
        }

        public async Task AddSkillAsync(Skill skill)
        {
            try
            {
                await ctx.Skills.AddAsync(skill);
               await  ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new JobSkillException(ex.InnerException.Message);
            }
        }

        public async Task<List<JobSkill>> GetAllJobSkillsAsync()
        {
            List<JobSkill> jobskills=await ctx.JobSkills.ToListAsync();
            return jobskills;
        }

        public async Task<JobSkill> GetJobSkillByJobIdandSkillIdAsync(string jobId, string skillId)
        {
            try
            {
                JobSkill jobskill = await (from js in ctx.JobSkills where js.JobId == jobId && js.SkillId == skillId select js).FirstAsync();
                return jobskill;
            }
            catch (Exception ex)
            {
                throw new JobSkillException(ex.Message);
            }
        }

        public async Task<List<JobSkill>> GetJobSkillsByJobIdAsync(string jobId)
        {
                List<JobSkill> jobskills = await (from js in ctx.JobSkills where js.JobId == jobId select js).ToListAsync();
                if (jobskills.Count > 0)
                {
                    return jobskills;
                }
                else
               {
                throw new JobSkillException("No JobSkill Found for given JobId");
               }
        }

        public async Task<List<JobSkill>> GetJobSkillsBySkillIdAsync(string skillId)
        {
            List<JobSkill> jobskills = await (from js in ctx.JobSkills where js.SkillId == skillId select js).ToListAsync();
            if (jobskills.Count > 0)
            {
                return jobskills;
            }
            else
            {
                throw new JobSkillException("No JobSkill Found for given SkillId");
            }
        }

        public async Task RemoveJobSkillAsync(string jobId, string skillId)
        {
            try
            {
                JobSkill jobskill = await (from js in ctx.JobSkills where js.JobId == jobId && js.SkillId == skillId select js).FirstAsync();
                ctx.JobSkills.Remove(jobskill);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new JobSkillException(ex.Message);
            }
        }
        public async Task RemoveJobAsync(string jobId)
        {
            try
            {
                Job job = await (from j in ctx.Jobs where j.JobId == jobId select j).FirstAsync();
                ctx.Jobs.Remove(job);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new JobSkillException(ex.InnerException.Message);
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
                throw new JobSkillException(ex.InnerException.Message);
            }
        }

        public async Task UpdateJobSkillAsync(string jobId, string skillId, JobSkill jobSkill)
        {
            try
            {
                JobSkill jobskill = await GetJobSkillByJobIdandSkillIdAsync(jobId, skillId);
                jobskill.Experience = jobSkill.Experience;
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new JobSkillException(ex.Message);
            }
        }
    }
}
