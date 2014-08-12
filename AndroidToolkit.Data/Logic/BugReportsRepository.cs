using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Data.Entities;

namespace AndroidToolkit.Data.Logic
{
    public class BugReportsRepository : GenericRepository<BugReport>, IBugReportsRepository<BugReport>
    {
        public BugReportsRepository(AndroidToolkitDB context)
            : base(context)
        {
        }
    }
}
