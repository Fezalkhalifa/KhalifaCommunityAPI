using KhalifaCommunityAPI.Models;
using KhalifaCommunityAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace KhalifaCommunity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : BaseController
    {
        private readonly KhalifaCommDBContext _entities;
        private readonly ILogger<RegistrationController> _logger;
        public RegistrationController(KhalifaCommDBContext entities, ILogger<RegistrationController> logger)
        {
            _entities = entities;
            _logger = logger;
        }
        [HttpGet(Name = "GetAllRegisteredList")]
        public ApiResponse GetAllRegisteredList()
        {
            try
            {

                var listdata = this._entities.Registrations.ToList();
                return Response(MessageType.Success, String.Empty, listdata);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public ApiResponse CreateRegistration([FromBody] Registration registration)
        {
            if (ModelState.IsValid)
            {
                Registration oldData = this._entities.Registrations.Where(x => x.RegistrationId == registration.RegistrationId).FirstOrDefault();
                if (oldData != null)
                {
                    if (this._entities.Registrations.Any(c => c.Email.Equals(oldData.Email)))
                        return Response(MessageType.Warning, $"User Already Exist with email address");
                    oldData.UpdatedDate = DateTime.Now;
                    oldData.Name = registration.Name;
                    oldData.Email = registration.Email;
                    oldData.Address = registration.Address;
                    oldData.City = registration.City;
                    oldData.Country = registration.Country;
                    registration.Users.UpdatedDate = DateTime.Now;
                    if (this._entities.SaveChanges() > 0)
                        return Response(MessageType.Success, $"Registration Details Updated Successfully");
                    else
                        return Response(MessageType.Error, $"An Error is Occured while Updating Entries");
                }
                else
                {
                    registration.CreatedDate = DateTime.Now;
                    registration.Users.CreatedDate = DateTime.Now;
                    this._entities.Registrations.Add(registration);
                    if (this._entities.SaveChanges() > 0)
                        return Response(MessageType.Success, $"Registration Details Updated Successfully");
                    else
                        return Response(MessageType.Error, $"An Error is Occured while Registration");
                }
            }
            else
                return Response(MessageType.Error, $"Please Enter Valid Data");
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public ApiResponse GetRegisteredUserById(int id)
        {
            if (id > 0)
            {
                Registration details = _entities.Registrations?.Where(x => x.RegistrationId == id).FirstOrDefault();
                if (details != null)
                    return Response(MessageType.Success, String.Empty, details);
                else
                    return Response(MessageType.Error, $"No User Found");
            }
            else
                return Response(MessageType.Warning, $"Please check the parameters");

        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public ApiResponse Login(string email)
        {
            bool hasUser = _entities.Registrations.Any(x => x.Email == email);

            if (hasUser)
                return Response(MessageType.Success, $"Login Successfully");
            else
                return Response(MessageType.NotFound, $"user Not Found");

        }
    }
}
