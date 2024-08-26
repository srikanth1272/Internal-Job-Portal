using JobSkillLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSkillLibrary.Repos
{
    public interface IJobSkillRepoAsync
    {
        Task AddJobSkillAsync(JobSkill jobSkill); 
        Task RemoveJobSkillAsync(string jobId, string skillId); 
        Task UpdateJobSkillAsync(string jobId, string skillId, JobSkill jobSkill); 
        Task<JobSkill> GetJobSkillByJobIdandSkillIdAsync(string jobId, string skillId); 
        Task<List<JobSkill>> GetAllJobSkillsAsync(); 
        Task<List<JobSkill>> GetJobSkillsByJobIdAsync(string jobId); 
        Task<List<JobSkill>> GetJobSkillsBySkillIdAsync(string skillId); 
        Task AddJobAsync(Job job); 
        Task RemoveJobAsync(string jobId);
        Task AddSkillAsync(Skill skill);
        Task RemoveSkillAsync(string skillId);


    }
}
