using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSkillLibrary.Models
{
    public class JobSkillException:Exception
    {
        public JobSkillException(string errMsg):base(errMsg) 
        {
            
        }
    }
}
