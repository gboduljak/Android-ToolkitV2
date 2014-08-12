using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AndroidToolkit.Data.Entities;
using AndroidToolkit.Data.Logic;
using Ninject;

namespace AndroidToolkit.Web.Api.Controllers
{
    [RoutePrefix("api/bug-reports")]
    public class BugReportsController : ApiController
    {
        [Inject]
        public BugReportsController(IBugReportsRepository<BugReport> repository)
        {
            _repository = repository;
        }

        [Route("get")]
        public IEnumerable<BugReport> GetBugReports()
        {
            return _repository.Get();
        }

        [Route("get")]
        public async Task<IHttpActionResult> GetBugReport(int id)
        {
            BugReport bugReport = await _repository.Get(id);
            if (bugReport == null)
            {
                return NotFound();
            }

            return Ok(bugReport);
        }

        [Authorize]
        [Route("edit/{id}")]
        public async Task<IHttpActionResult> PutBugReport(int id, BugReport bugReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bugReport.Id)
            {
                return BadRequest();
            }

            _repository.Update(bugReport);

            await _repository.Save();

            return Ok(bugReport);
        }

        [Authorize]
        [Route("create")]
        public async Task<IHttpActionResult> PostBugReport(BugReport bugReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.Insert(bugReport);

            await _repository.Save();

            return CreatedAtRoute("DefaultApi", new { id = bugReport.Id }, bugReport);
        }

        [Authorize(Users = "gboduljak")]
        [Route("delete/{id}")]
        public async Task<IHttpActionResult> DeleteBugReport(int id)
        {
            BugReport bugReport = await _repository.Get(id);
            if (bugReport == null)
            {
                return NotFound();
            }

            _repository.Delete(bugReport);
            await _repository.Save();
            return Ok(bugReport);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
                _repository = null;
            }
            base.Dispose(disposing);
        }

        private IBugReportsRepository<BugReport> _repository;
    }
}