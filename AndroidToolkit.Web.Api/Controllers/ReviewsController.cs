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
using System.Web.Http.Description;
using System.Web.Http.OData;
using AndroidToolkit.Data.Entities;
using AndroidToolkit.Data.Logic;
using Ninject;

namespace AndroidToolkit.Web.Api.Controllers
{
    [RoutePrefix("api/reviews")]
    public class ReviewsController : ApiController
    {
        [Inject]
        public ReviewsController(IReviewsRepository<Review> repository)
        {
            _repository = repository;
        }


        [Route("get")]
        [EnableQuery]
        public IEnumerable<Review> GetReviews()
        {
            return _repository.Get();
        }


        [EnableQuery]
        [Route("get/{id}")]
        public async Task<IHttpActionResult> GetReview(int id)
        {
            Review review = await _repository.Get(id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        [Authorize(Users = "gboduljak")]
        [Route("edit/{id}")]
        public async Task<IHttpActionResult> PutReview(int id, Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != review.Id)
            {
                return BadRequest();
            }

            _repository.Update(review);
            await _repository.Save();

            return Ok(review);
        }


        [Authorize]
        [Route("create")]
        public async Task<IHttpActionResult> PostReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Insert(review);
            await _repository.Save();

            return CreatedAtRoute("DefaultApi", new { id = review.Id }, review);
        }

        [Authorize(Users = "gboduljak")]
        [Route("delete/{id}")]
        public async Task<IHttpActionResult> DeleteReview(int id)
        {
            Review review = await _repository.Get(id);
            if (review == null)
            {
                return NotFound();
            }

            _repository.Delete(review);
            await _repository.Save();

            return Ok(review);
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

        private IReviewsRepository<Review> _repository;
    }
}