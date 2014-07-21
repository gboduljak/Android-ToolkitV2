using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Data.Entities;

namespace AndroidToolkit.Data.Logic
{
    public interface IDeviceRepository : IDisposable
    {
        IQueryable<Device> Get();
        Task<Device> Get(int id);
        Task<Device> Get(string name);
        Task<IEnumerable<Recovery>> GetRecoveries(int id);
        Task<bool> Create(Device device);
        Task<bool> AddRecovery(int deviceId, int recoveryId);
        Task<bool> Edit(Device device);
        Task<bool> Delete(int id);
    }
}
