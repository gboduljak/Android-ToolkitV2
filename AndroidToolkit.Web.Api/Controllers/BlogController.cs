using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using System.Web.Http.Results;
using AndroidToolkit.Data.Entities;
using AndroidToolkit.Data.Logic;
using Ninject;

namespace AndroidToolkit.Web.Api.Controllers
{
    [RoutePrefix("api/blog")]
    public class BlogController : ApiController
    {
        [Inject]
        public BlogController(IBlogRepository<Post> repository)
        {
            _repository = repository;
        }

        private IBlogRepository<Post> _repository;

        [Route("get")]
        [EnableQuery]
        public IEnumerable<Post> GetBlog()
        {
            return _repository.Get();
        }

        [Route("get/{id}")]
        [EnableQuery]
        public async Task<IHttpActionResult> Get(int id)
        {
            var post = await _repository.Get(id);
            if (post != null)
            {
                return Ok(post);
            }
            return BadRequest("Blog post with id:" + id + "doesn't exist");
        }

        [Authorize(Users = "gboduljak")]
        [Route("edit/{id}")]
        public async Task<IHttpActionResult> Put(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.Id)
            {
                return BadRequest();
            }

            _repository.Update(post);
            await _repository.Save();
            return Ok(post);
        }

        // POST: odata/Blog
        [Authorize(Users = "gboduljak")]
        [Route("api/create")]
        public async Task<IHttpActionResult> Post(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Insert(post);
            await _repository.Save();

            return Ok(post);
        }

        [Authorize(Users = "gboduljak")]
        [Route("delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Post post = await _repository.Get(id);
            if (post == null)
            {
                return NotFound();
            }

            _repository.Delete(post);
            await _repository.Save();

            return Ok();
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


    }
}
