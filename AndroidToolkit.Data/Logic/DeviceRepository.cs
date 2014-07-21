using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Data.Entities;

namespace AndroidToolkit.Data.Logic
{
    public class DeviceRepository : IDeviceRepository
    {
        public DeviceRepository(AndroidToolkitDB db)
        {
            _db = db;
        }

        public IQueryable<Device> Get()
        {
            return _db.Devices.AsQueryable();
        }

        public Task<Device> Get(int id)
        {
            return _db.Devices.FindAsync(id);
        }

        public Task<Device> Get(string name)
        {
            return _db.Devices.FirstAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Recovery>> GetRecoveries(int id)
        {
            var device = await Get(id);
            await _db.Entry(device).Collection(d => d.Recoveries).LoadAsync();
            return device.Recoveries;
        }

        public async Task<bool> Create(Device device)
        {
            _db.Devices.Add(device);
            await Save();
            return true;
        }

        public async Task<bool> AddRecovery(int deviceId, int recoveryId)
        {
            Device device = await Get(deviceId);
            device.Recoveries.Add(await _db.Recoveries.FindAsync(recoveryId));
            await Save();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            Device device = await Get(id);
            _db.Devices.Remove(device);
            await Save();
            return true;
        }

        public async Task<bool> Edit(Device device)
        {
            _db.Entry(device).State = EntityState.Modified;
            await Save();
            return true;
        }

        private Task Save()
        {
            return _db.SaveChangesAsync();
        }

        private AndroidToolkitDB _db;

        ~DeviceRepository()
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
