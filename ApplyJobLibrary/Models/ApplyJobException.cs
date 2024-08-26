using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplyJobLibrary.Models
{
    public class ApplyJobException:Exception
    {
        public ApplyJobException(string errMsg):base(errMsg) { }
    }
}
