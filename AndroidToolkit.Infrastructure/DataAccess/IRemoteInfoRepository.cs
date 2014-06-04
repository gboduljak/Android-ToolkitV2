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
        Task<IList<RemoteInfo>> Get();
        Task<bool> Add(RemoteInfo info);
        Task<bool> Delete(string address);
    }
}
