using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Data.Entities;

namespace AndroidToolkit.Data.Logic
{
    public interface IRecoveriesRepository : IDisposable
    {
        IQueryable<Recovery> Get();
        Task<Recovery> Get(int id);
        Task<Recovery> Get(string name);

        Task<Device> GetDevice(int id);
        Device GetDevice(Recovery recovery);

        Task<bool> Create(Recovery recovery);
        Task<bool> Edit(Recovery recovery);
        Task<bool> Delete(int id);

    }
}
