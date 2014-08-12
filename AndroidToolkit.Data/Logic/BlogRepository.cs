using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Data.Entities;

namespace AndroidToolkit.Data.Logic
{
    public class BlogRepository : GenericRepository<Post>, IBlogRepository<Post>
    {
        public BlogRepository(AndroidToolkitDB context)
            : base(context)
        {

        }

    }
}
