using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Webapi.Controllers
{
    public class PolicyController : ApiController
    {
        InsuranceDbContext db = new InsuranceDbContext();
        [HttpGet]
        public IEnumerable<Policy> getAll()
        {
            return db.Policies.ToList();
        }
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var policy = db.Policies.SingleOrDefault(x => x.PolicyId == id);
            if (policy == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(policy);
            }
        }
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Policy policy)
        {
            var policy1 = db.Policies.SingleOrDefault(e => e.PolicyId == id);
            if (policy1 == null)
            {
                return NotFound();
            }
            else
            {
                policy1.PolicyNumber = policy.PolicyNumber;
                policy1.PolicyId = policy.PolicyId;
                policy1.policycategory = policy.policycategory;
                policy1.PolicyNumber = policy.PolicyNumber;
                policy1.Price = policy.Price;
                policy1.CustomerId = policy.CustomerId;
                policy1.DateOfCreation = policy.DateOfCreation;
                policy1.Category = policy.Category;
                db.SaveChanges();
                return Ok();
            }
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var policy = db.Policies.SingleOrDefault(e => e.PolicyId == id);
            if (policy == null)
            {
                return NotFound();
            }
            else
            {
                db.Policies.Remove(policy);
                db.SaveChanges();
                return Ok();
            }
        }
        [HttpPost]
        public IHttpActionResult Post([FromBody] Policy policy)
        {

            if (policy == null)
            {

                return BadRequest();
            }
            else
            {
                db.Policies.Add(policy);
                db.SaveChanges();
                return Ok();
            }
        }
    }
}