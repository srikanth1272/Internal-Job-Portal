using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillLibrary.Models
{
    public class EmployeeSkillException :Exception
    {
        public EmployeeSkillException(string errMsg):base(errMsg) { }
    }
}
