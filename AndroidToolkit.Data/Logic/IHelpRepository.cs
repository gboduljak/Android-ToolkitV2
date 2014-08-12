using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Data.Entities;

namespace AndroidToolkit.Data.Logic
{
    public interface IHelpRepository<in TEntity> where TEntity : Help
    {
        IEnumerable<Help> Get(
        Expression<Func<Help, bool>> filter = null,
        Func<IQueryable<Help>, IOrderedQueryable<Help>> orderBy = null,
        string includeProperties = "");

        Task<Help> Get(object id);

        Task Save();

        void Insert(Help entity);

        void Delete(object id);

        void Delete(Help entityToDelete);

        void Update(Help entityToUpdate);

        void Dispose();
    }
}
