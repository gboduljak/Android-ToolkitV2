using System.Collections;
using System.Windows.Controls;
using System.Windows.Forms;
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
        public RemoteInfoRepository()
        {
            _infos = new List<RemoteInfo>();
        }
        private static void CreateFile()
        {
            File.WriteAllText("remote.json","[]");
        }
        public Task<IList<RemoteInfo>> Get()
        {
            return Task.Run(() =>
            {
                if (!File.Exists("remote.json"))
                {
                    CreateFile();
                }
                _infos = JsonConvert.DeserializeObject<List<RemoteInfo>>(File.ReadAllText("remote.json"));
                return _infos;
            });
        }

        public Task<bool> Add(RemoteInfo info)
        {
            return Task.Run(() =>
            {
                if (!File.Exists("remote.json"))
                {
                    CreateFile();
                }
                _infos.Add(info);
                File.WriteAllText("remote.json", JsonConvert.SerializeObject(_infos));
                return true;

            });
        }

        public Task<bool> Delete(string address)
        {
            return Task.Run(() =>
            {
                if (!File.Exists("remote.json"))
                {
                    CreateFile();
                }
                _infos = JsonConvert.DeserializeObject<List<RemoteInfo>>(File.ReadAllText("remote.json"));
                _infos.Remove(_infos.First(x => x.Address == address));
                File.WriteAllText("remote.json", JsonConvert.SerializeObject(_infos));
                return true;
            });
        }

        private IList<RemoteInfo> _infos;
    }
}