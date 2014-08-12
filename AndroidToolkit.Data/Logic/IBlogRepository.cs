using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Data.Entities;

namespace AndroidToolkit.Data.Logic
{
    public interface IBlogRepository<in TEntity> where TEntity : Post
    {
        IEnumerable<Post> Get(
            Expression<Func<Post, bool>> filter = null,
            Func<IQueryable<Post>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<Post> Get(object id);

        Task Save();

        void Insert(Post entity);

        void Delete(object id);

        void Delete(Post entityToDelete);

        void Update(Post entityToUpdate);

        void Dispose();
    }
}
