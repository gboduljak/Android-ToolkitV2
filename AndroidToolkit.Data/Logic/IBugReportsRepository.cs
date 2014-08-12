using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Data.Entities;

namespace AndroidToolkit.Data.Logic
{
    public interface IBugReportsRepository<in TEntity> where TEntity : BugReport
    {
        IEnumerable<BugReport> Get(
        Expression<Func<BugReport, bool>> filter = null,
        Func<IQueryable<BugReport>, IOrderedQueryable<BugReport>> orderBy = null,
        string includeProperties = "");

        Task<BugReport> Get(object id);

        Task Save();

        void Insert(BugReport entity);

        void Delete(object id);

        void Delete(BugReport entityToDelete);

        void Update(BugReport entityToUpdate);

        void Dispose();
    }
}
