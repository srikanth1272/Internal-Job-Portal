using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillLibrary.Repos
{
    public class SkillException:Exception
    {
        public SkillException(string msg):base(msg)
        {
            
        }
    }
}
