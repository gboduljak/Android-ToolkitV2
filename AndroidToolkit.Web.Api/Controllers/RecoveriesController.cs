using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AndroidToolkit.Data.Logic;

namespace AndroidToolkit.Web.Api.Controllers
{
    public class RecoveriesController : ApiController
    {

        public RecoveriesController()
        {
            _repository=new DeviceRepository();
        }


        #region Fields

        private IDeviceRepository _repository;

        #endregion
    }
}
