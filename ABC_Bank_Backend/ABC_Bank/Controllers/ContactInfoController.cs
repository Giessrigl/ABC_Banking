using ABC_Bank.Extensions;
using ABC_Bank.Models;
using ABC_Bank.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ABC_Bank.Controllers
{
    [RoutePrefix("api/contactinfo")]
    public class ContactInfoController : ApiController
    {
        private readonly ContactInfoRepository contactRepo;

        internal ContactInfoController()
        {
            this.contactRepo = new ContactInfoRepository();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetContactInfo()
        {
            IHttpActionResult result = default;

            string searchPattern = Request.GetQueryNameValuePairs().FirstOrDefault(x => x.Key == "searchPattern").Value;
            string category = Request.GetQueryNameValuePairs().FirstOrDefault(x => x.Key == "category").Value;

            try
            {
                searchPattern.ThrowIfNullOrEmpty(nameof(searchPattern));
                category.ThrowIfNullOrEmpty(nameof(category));

                IEnumerable<ContactInfo> foundEntities = new List<ContactInfo>();
                switch (category)
                {
                    case "name":
                        foundEntities = this.contactRepo.GetContactsByName(searchPattern);
                        break;

                    case "address":
                        foundEntities = this.contactRepo.GetContactsByAddress(searchPattern);
                        break;

                    case "age":
                        foundEntities = this.contactRepo.GetContactsByAge(searchPattern);
                        break;
                }

                result = Ok(foundEntities);
            }
            catch (ArgumentNullException e)
            {
                result = BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = InternalServerError(new Exception("Could not search for contact info by name."));
            }

            return await Task.FromResult(result);
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddContactInfo([FromBody] ContactInfo contactToAdd)
        {
            IHttpActionResult result;

            try
            {
                contactToAdd.ThrowIfNull(nameof(contactToAdd));
                contactToAdd.ValidateContactInfo();

                if (!DateTime.TryParse(contactToAdd.DateOfBirth, out var date))
                    result = BadRequest("Date of Birth is not a date. Try the format: 01 Jan 1990");

                if (this.contactRepo.AddContact(contactToAdd))
                    result = Ok("Contactinfo has been submitted successfully.");
                else
                    throw new Exception();
            }
            catch (ArgumentNullException e)
            {
                result = BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = InternalServerError(new Exception("Add contact: failed."));
            }

            return await Task.FromResult(result);
        }
    }
}
