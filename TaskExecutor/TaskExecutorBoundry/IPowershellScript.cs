using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskExecutorBoundry
{
    public interface IPowershellScript
    {
        string ExecuteScript(string command);
    }
}
