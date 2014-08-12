using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Data.Entities;

namespace AndroidToolkit.Data.Logic
{
    public class ReviewsRepository : GenericRepository<Review>, IReviewsRepository<Review>
    {
        public ReviewsRepository(AndroidToolkitDB context) : base(context)
        {
        }
    }
}
