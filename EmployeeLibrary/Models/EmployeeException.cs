using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLibrary.Models
{
     public class EmployeeException : Exception
    {
        public EmployeeException(string errMsg):base(errMsg) { }
    }
}
