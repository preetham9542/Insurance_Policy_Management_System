using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Webapi.Controllers
{
    public class CustomerController : ApiController
    {
        InsuranceDbContext db = new InsuranceDbContext();
        [HttpGet]
        public IEnumerable<Customer> getAll()
        {
            return db.Customers.ToList();
        }
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var customer = db.Customers.SingleOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(customer);
            }
        }
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Customer customer)
        {
            var customer1 = db.Customers.SingleOrDefault(e => e.Id == id);
            if (customer1 == null)
            {
                return NotFound();
            }
            else
            {
                customer1.Id = customer.Id;
                customer1.FirstName = customer.FirstName;
                customer1.LastName = customer.LastName;
                customer1.Email = customer.Email;
                customer1.PhoneNumber = customer.PhoneNumber;
                customer1.UserName = customer.UserName;
                customer1.Password = customer.Password;
                customer1.RoleId = customer.RoleId;
                db.SaveChanges();
                return Ok();
            }
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var customer = db.Customers.SingleOrDefault(e => e.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {

                db.Customers.Remove(customer);
                db.SaveChanges();
                return Ok();
            }
        }
        [HttpPost]
        public IHttpActionResult Post([FromBody] Customer customer)
        {

            if (customer == null)
            {

                return BadRequest();
            }
            else
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return Ok();
            }

        }
    }
}