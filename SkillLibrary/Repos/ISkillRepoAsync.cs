using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillLibrary.Models;

namespace SkillLibrary.Repos
{
    public interface ISkillRepoAsync
    {
            Task AddSkillDetailsAsync(Skill skill);
            Task RemoveSkillDetailsAsync(string skillId);
            Task UpdateSkillDetailsAsync(string skillId, Skill skill);
            Task<Skill> GetSkillAsync(string skillId);
            Task<List<Skill>> GetAllSkillDetailsAsync();
            Task<List<Skill>> GetSkillsByLevelAsync(string skillLevel);
            Task<List<Skill>> GetSkillsByCategoryAsync(string category);

    }
}
