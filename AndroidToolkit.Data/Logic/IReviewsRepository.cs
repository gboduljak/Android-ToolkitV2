using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Data.Entities;

namespace AndroidToolkit.Data.Logic
{
    public interface IReviewsRepository<in TEntity> where TEntity : Review
    {
        IEnumerable<Review> Get(
         Expression<Func<Review, bool>> filter = null,
         Func<IQueryable<Review>, IOrderedQueryable<Review>> orderBy = null,
         string includeProperties = "");

        Task<Review> Get(object id);

        Task Save();

        void Insert(Review entity);

        void Delete(object id);

        void Delete(Review entityToDelete);

        void Update(Review entityToUpdate);

        void Dispose();
    }
}
