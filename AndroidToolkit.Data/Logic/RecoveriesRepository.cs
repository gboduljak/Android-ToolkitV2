
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Data.Entities;

namespace AndroidToolkit.Data.Logic
{
    public class RecoveriesRepository : IRecoveriesRepository
    {

        public IQueryable<Recovery> Get()
        {
            return _db.Recoveries.AsQueryable();
        }

        public Task<Recovery> Get(int id)
        {
            return _db.Recoveries.FindAsync(id);
        }

        public Task<Recovery> Get(string name)
        {
            return _db.Recoveries.FirstAsync(x => x.Name == name);
        }

        public async Task<Device> GetDevice(int id)
        {
            Recovery recovery = await Get(id);
            return recovery.Device;
        }

        public Device GetDevice(Recovery recovery)
        {
            return recovery.Device;
        }

        public async Task<bool> Create(Recovery recovery)
        {
            _db.Recoveries.Add(recovery);
            try
            {
                await Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Edit(Recovery recovery)
        {
            _db.Entry(recovery).State = EntityState.Modified;
            try
            {
                await Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            Recovery recovery = await Get(id);
            _db.Recoveries.Remove(recovery);
            try
            {
                await Save();
                return true;
            }
            catch
            {
                return false;
            }
        }


        private Task Save()
        {
            return _db.SaveChangesAsync();
        }

        private AndroidToolkitDB _db;

        ~RecoveriesRepository()
        {
            Dispose(false);
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db = null;
                    GC.Collect();
                    GC.SuppressFinalize(this);
                }
            }
            _disposed = true;
        }

        private bool _disposed;

        #endregion
    }
}
