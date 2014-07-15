using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using AndroidToolkit.Data.Entities;
using AndroidToolkit.Data.Logic;
using AndroidToolkit.Web.Api.Models;

namespace AndroidToolkit.Web.Api.Controllers
{
    [RoutePrefix("api/devices")]
    public class DevicesController : ApiController
    {
        public DevicesController()
        {
            _repo = new DeviceRepository();
            _repo2 = new RecoveriesRepository();
        }

        [Route("get")]
        [AllowAnonymous]
        [EnableQuery]
        public IQueryable<Device> Get()
        {
            return _repo.Get();
        }

        [Route("get")]
        [AllowAnonymous]
        public Task<Device> Get(int id)
        {
            return _repo.Get(id);
        }

        [Route("get")]
        [AllowAnonymous]
        public Task<Device> Get(string name)
        {
            return _repo.Get(name);
        }

        [Route("get-recoveries")]
        [AllowAnonymous]
        [HttpGet]
        [EnableQuery]
        public async Task<IEnumerable> GetRecoveries(int deviceId)
        {
            var device = await _repo.Get(deviceId);
            IList recoveries = new List<object>();
            if (device != null)
            {
                foreach (var recovery in device.Recoveries)
                {
                    recoveries.Add(new { recovery.Id, recovery.Name, recovery.Version });
                }
            }
            return recoveries;
        }

        [Route("create")]
        [Authorize(Users = "gboduljak")]
        [HttpPost]
        public async Task<IHttpActionResult> Create(CreateDeviceBindingModel model)
        {
            var device = new Device() { Name = model.Name, Manufacturer = model.Manufacturer, Year = model.Year };
            foreach (int recoveryId in model.RecoveryIds)
            {
                Recovery temp = await _repo2.Get(recoveryId);
                device.Recoveries.Add(temp);
            }
            if (await _repo.Create(device))
            {
                return Ok(device);
            }
            return BadRequest("Unknown error");
        }

        [Route("edit")]
        [Authorize(Users = "gboduljak")]
        [HttpPut]
        public async Task<IHttpActionResult> Edit(EditDeviceBindingModel model)
        {
            var device = await _repo.Get(model.Id);
            device.Name = model.Name;
            device.Manufacturer = model.Manufacturer;
            device.Year = model.Year;
            device.Image = model.Image;
            foreach (int recoveryId in model.RecoveryIds)
            {
                Recovery temp = await _repo2.Get(recoveryId);
                device.Recoveries.Clear();
                device.Recoveries.Add(temp);
            }
            await _repo.Edit(device);
            return Ok(device);
        }

        [Route("delete")]
        [Authorize(Users = "gboduljak")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _repo.Delete(id);
            return Ok();
        }

        #region Fields

        private IDeviceRepository _repo;

        private IRecoveriesRepository _repo2;

        #endregion

        ~DevicesController()
        {
            Dispose(false);
        }

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repo = null;
                    _repo2 = null;
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
