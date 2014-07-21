using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using AndroidToolkit.Data.Entities;
using AndroidToolkit.Data.Logic;
using AndroidToolkit.Web.Api.Models;
using Ninject;

namespace AndroidToolkit.Web.Api.Controllers
{
    [RoutePrefix("api/recoveries")]
    public class RecoveriesController : ApiController
    {
        [Inject]
        public RecoveriesController(IDeviceRepository repo2, IRecoveriesRepository repo)
        {
            _repo = repo;
            _repo2 = repo2;
        }

        [Route("get")]
        [AllowAnonymous]
        [EnableQuery]
        public List<ShowRecoveryModel> Get()
        {
            List<ShowRecoveryModel> viewModels = new List<ShowRecoveryModel>();
            foreach (var recovery in _repo.Get())
            {
                ShowRecoveryModel viewModel = new ShowRecoveryModel()
                {
                    Id = recovery.Id,
                    Name = recovery.Name,
                    Version = recovery.Version,
                    Download = recovery.Download,
                };
                if (recovery.Device != null)
                {
                    viewModel.Device = new
                    {
                        Id = recovery.Device.Id,
                        Name = recovery.Device.Name,
                        Manufacturer = recovery.Device.Manufacturer,
                        Year = recovery.Device.Year,
                        Image = recovery.Device.Image
                    };
                }
                viewModels.Add(viewModel);
            }
            return viewModels;
        }

        [Route("get")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get(int id)
        {
            Recovery recovery = await _repo.Get(id);
            if (recovery != null)
            {
                ShowRecoveryModel viewModel = new ShowRecoveryModel()
                {
                    Id = recovery.Id,
                    Name = recovery.Name,
                    Version = recovery.Version,
                    Download = recovery.Download,
                };
                if (recovery.Device != null)
                {
                    viewModel.Device = new
                    {
                        Id = recovery.Device.Id,
                        Name = recovery.Device.Name,
                        Manufacturer = recovery.Device.Manufacturer,
                        Year = recovery.Device.Year,
                        Image = recovery.Device.Image
                    };
                }
                return Ok(viewModel);
            }
            return BadRequest();
        }

        [Route("get")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get(string name)
        {
            Recovery recovery = await _repo.Get(name);
            if (recovery != null)
            {
                ShowRecoveryModel viewModel = new ShowRecoveryModel()
                {
                    Id = recovery.Id,
                    Name = recovery.Name,
                    Version = recovery.Version,
                    Download = recovery.Download,
                };
                if (recovery.Device != null)
                {
                    viewModel.Device = new
                    {
                        Id = recovery.Device.Id,
                        Name = recovery.Device.Name,
                        Manufacturer = recovery.Device.Manufacturer,
                        Year = recovery.Device.Year,
                        Image = recovery.Device.Image
                    };
                }
                return Ok(viewModel);
            }
            return BadRequest();
        }

        [Route("get-device")]
        [AllowAnonymous]
        [HttpGet]
        [EnableQuery]
        public async Task<IHttpActionResult> GetDevice(int recoveryId)
        {
            var recovery = await _repo.Get(recoveryId);
            if (recovery != null && recovery.Device != null)
            {
                var vm = new ShowDeviceModel()
                {
                    Id = recovery.Device.Id,
                    Image = recovery.Device.Image,
                    Manufacturer = recovery.Device.Manufacturer,
                    Name = recovery.Device.Name,
                    Year = recovery.Device.Year
                };
                foreach (var rec in await _repo2.GetRecoveries(vm.Id))
                {
                    vm.Recoveries.Add(rec);
                }
                return Ok(vm);
            }
            return NotFound();
        }

        [Route("create")]
        [Authorize(Users = "gboduljak")]
        [HttpPost]
        public async Task<IHttpActionResult> Create(CreateRecoveryModel model)
        {
            if (model != null)
            {
                Recovery recovery = new Recovery()
                {
                    Name = model.Name,
                    Version = model.Version,
                    Download = model.Download,
                    DeviceId = model.DeviceId
                };
                if (await _repo.Create(recovery))
                {
                    return Ok();
                }
                return InternalServerError(new Exception("An error occured while creating recovery."));
            }
            return BadRequest();
        }

        [Route("edit")]
        [Authorize(Users = "gboduljak")]
        [HttpPut]
        public async Task<IHttpActionResult> Edit(EditRecoveryModel model)
        {
            if (model != null)
            {
                Recovery recovery = await _repo.Get(model.Id);
                if (recovery != null)
                {
                    recovery.Name = model.Name;
                    recovery.Version = model.Version;
                    recovery.Download = model.Download;
                    recovery.DeviceId = model.DeviceId;
                    if (await _repo.Edit(recovery))
                    {
                        return Ok();
                    }
                }
                return NotFound();
            }
            return BadRequest();
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

        private IRecoveriesRepository _repo;

        private IDeviceRepository _repo2;

        #endregion

        ~RecoveriesController()
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
