using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Data.Entities;

namespace AndroidToolkit.Data.Logic
{
    public class HelpsRepository : GenericRepository<Help>, IHelpRepository<Help>
    {
        public HelpsRepository(AndroidToolkitDB context)
            : base(context)
        {

        }
    }
}
