using AndroidToolkit.Infrastructure.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidToolkit.Infrastructure.DataAccess
{
    public interface IRemoteInfoRepository
    {
        Task<IEnumerable<RemoteInfo>> Get();
        Task Add(RemoteInfo info);
        Task Delete(int id);
    }
}
