using AndroidToolkit.Infrastructure.Device;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AndroidToolkit.Infrastructure.DataAccess
{
    public class RemoteInfoRepository : IRemoteInfoRepository
    {
        public Task<IEnumerable<RemoteInfo>> Get()
        {
            return Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<RemoteInfo>>(File.ReadAllText("remote.json")));
        }

        public Task Add(RemoteInfo info)
        {
            return Task.Run(() => { File.WriteAllText("remote.json", JsonConvert.SerializeObject(info)); });
        }

        public Task Delete(int id)
        {
            return Task.Run(() =>
            {
                List<RemoteInfo> remoteInfos = JsonConvert.DeserializeObject<List<RemoteInfo>>(File.ReadAllText("remote.json"));
                remoteInfos.Remove(remoteInfos.First(x => x.ID == id));
                File.WriteAllText("remote.json", JsonConvert.SerializeObject(remoteInfos));
            });
        }
    }
}