using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLibrary.Repos
{
    public class JobException:Exception
    {
        public JobException(string errMsg):base(errMsg) 
        {
            
        }
    }
}
