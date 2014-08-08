using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using AndroidToolkit.Data.Entities;
using AndroidToolkit.Data.Logic;
using AndroidToolkit.Web.Api.Models;
using AndroidToolkit.Web.Api.Providers;
using Ninject;

namespace AndroidToolkit.Web.Api.Controllers
{
    [RoutePrefix("api/devices")]
    //[EnableCustomCors]
    public class DevicesController : ApiController
    {
        [Inject]
        public DevicesController(IDeviceRepository repo, IRecoveriesRepository repo2)
        {
            _repo = repo;
            _repo2 = repo2;
        }

       
        [Authorize]
        [Route("get")]
        //[AllowAnonymous]
        [EnableQuery]
        public async Task<List<ShowDeviceModel>> Get()
        {
            List<ShowDeviceModel> viewModels = new List<ShowDeviceModel>();
            foreach (var vm in _repo.Get().Select(device => new ShowDeviceModel()
            {
                Id = device.Id,
                Image = device.Image,
                Manufacturer = device.Manufacturer,
                Name = device.Name,
                Year = device.Year
            }))
            {
                var recoveries = await _repo.GetRecoveries(vm.Id);
                foreach (var recovery in recoveries)
                {
                    vm.Recoveries.Add(new { Name = recovery.Name, Version = recovery.Version, Download = recovery.Download });
                }
                viewModels.Add(vm);
            }
            return viewModels;
        }


        [Route("get")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get(int id)
        {
            Device device = await _repo.Get(id);
            if (device != null)
            {
                ShowDeviceModel vm = new ShowDeviceModel()
                {
                    Id = device.Id,
                    Image = device.Image,
                    Manufacturer = device.Manufacturer,
                    Name = device.Name,
                    Year = device.Year
                };
                var recoveries = await _repo.GetRecoveries(vm.Id);
                foreach (var recovery in recoveries)
                {
                    vm.Recoveries.Add(new { Name = recovery.Name, Version = recovery.Version, Download = recovery.Download });
                }
                return Ok(vm);
            }
            return BadRequest();
        }

        [Route("get")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get(string name)
        {
            Device device = await _repo.Get(name);
            if (device != null)
            {
                ShowDeviceModel vm = new ShowDeviceModel()
                {
                    Id = device.Id,
                    Image = device.Image,
                    Manufacturer = device.Manufacturer,
                    Name = device.Name,
                    Year = device.Year
                };
                var recoveries = await _repo.GetRecoveries(vm.Id);
                foreach (var recovery in recoveries)
                {
                    vm.Recoveries.Add(new { Name = recovery.Name, Version = recovery.Version, Download = recovery.Download });
                }
                return Ok(vm);
            }
            return BadRequest();
        }

        [Route("get-recoveries")]
        [AllowAnonymous]
        [HttpGet]
        [EnableQuery]
        public async Task<IHttpActionResult> GetRecoveries(int deviceId)
        {
            var device = await _repo.Get(deviceId);
            if (device != null)
            {
                IList recoveries = new List<object>();

                foreach (var recovery in device.Recoveries)
                {
                    recoveries.Add(new { Id = recovery.Id, Name = recovery.Name, Version = recovery.Version });
                }

                return Ok(recoveries);
            }
            return BadRequest();
        }

        [Route("create")]
        [Authorize(Users = "gboduljak")]
        [HttpPost]
        public async Task<IHttpActionResult> Create(CreateDeviceBindingModel model)
        {
            if (model != null)
            {
                var device = new Device { Name = model.Name, Manufacturer = model.Manufacturer, Year = model.Year, Image = model.Image };
                if (model.RecoveryIds != null)
                {
                    foreach (int recoveryId in model.RecoveryIds)
                    {
                        Recovery temp = await _repo2.Get(recoveryId);
                        if (temp != null)
                        {
                            device.Recoveries.Add(temp);
                        }
                    }
                }
                if (await _repo.Create(device))
                {
                    return Ok();
                }
            }
            return BadRequest("Model error.");
        }

        [Route("edit")]
        [Authorize(Users = "gboduljak")]
        [HttpPut]
        public async Task<IHttpActionResult> Edit(EditDeviceBindingModel model)
        {
            if (model != null)
            {
                var device = await _repo.Get(model.Id);
                if (device != null)
                {
                    device.Name = model.Name;
                    device.Manufacturer = model.Manufacturer;
                    device.Year = model.Year;
                    device.Image = model.Image;
                    foreach (int recoveryId in model.RecoveryIds)
                    {
                        Recovery temp = await _repo2.Get(recoveryId);
                        if (temp != null)
                        {
                            device.Recoveries.Clear();
                            device.Recoveries.Add(temp);
                        }
                    }
                    if (await _repo.Edit(device))
                    {
                        return Ok();
                    }
                    return NotFound();
                }
            }
            return BadRequest("Model error.");
        }

        [Route("delete")]
        [Authorize(Users = "gboduljak")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id > 0)
            {
                if (await _repo.Delete(id))
                {
                    return Ok();
                }
                return InternalServerError(new Exception("An server error occured while deleting recovery."));
            }
            return BadRequest("Parameter 'id' is required.");
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
