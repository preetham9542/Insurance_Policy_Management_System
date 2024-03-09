using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Webapi.Controllers
{
    public class AdminController : ApiController
    {
        InsuranceDbContext db = new InsuranceDbContext();
        [HttpGet]
        public IEnumerable<Admin> getAll()
        {
            return db.Admins.ToList();
        }
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var admin = db.Admins.SingleOrDefault(x => x.Id == id);
            if (admin == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(admin);
            }
        }
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Admin admin)
        {
            var admin1 = db.Admins.SingleOrDefault(e => e.Id == id);
            if (admin1 == null)
            {
                return NotFound();
            }
            else
            {
                admin1.Id = admin.Id;
                admin1.FirstName = admin.FirstName;
                admin1.LastName = admin.LastName;
                admin1.EmailAddress = admin.EmailAddress;
                admin1.PhoneNumber = admin.PhoneNumber;
                admin1.UserName = admin.UserName;
                admin1.Password = admin.Password;
                admin1.RoleId = admin.RoleId;
                db.SaveChanges();
                return Ok();
            }
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var admin = db.Admins.SingleOrDefault(e => e.Id == id);
            if (admin == null)
            {
                return NotFound();
            }
            else
            {

                db.Admins.Remove(admin);
                db.SaveChanges();
                return Ok();
            }
        }
        [HttpPost]
        public IHttpActionResult Post([FromBody] Admin admin)
        {

            if (admin == null)
            {

                return BadRequest();
            }
            else
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return Ok();
            }

        }
    }
}