using System;
using System.Collections.Generic;
using System.Linq;
using KonnektuTask.API.Factories;
using KonnektuTask.API.Models;
using KonnektuTask.API.Models.Request;
using KonnektuTask.API.Tools;
using KonnektuTask.Core;
using KonnektuTask.EF;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace KonnektuTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IPasswordHasher _hasher;
        private readonly ResponseFactory _responseFactory;

        public RegistrationController(ApplicationContext context, IPasswordHasher hasher, ResponseFactory responseFactory)
        {
            _context = context;
            _hasher = hasher;
            _responseFactory = responseFactory;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OkObjectResult), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public IActionResult RegisterUser(BaseRequest<RegisterUserRequest> data)
        {
            Guid sourceId = Guid.Parse(data.Request.SourceId);
            
            var source = _context.Sources.Find(sourceId);
            if (source == null || source.SecretKey != data.Request.SourceSecretKey)
            {
                ModelState.AddModelError("SecretKey", "Wrong Secret key");
                return BadRequest(_responseFactory.CreateFailureResponse(data.Request.SourceId, model: ModelState));
            }

            var userData = data.Request.UserData;
            var createdUser = new User()
            {
                Email = userData.Email,
                Phone = userData.Phone,
                Surname = userData.Surname,
                BirthDate = userData.BirthDate,
                GenderId = userData.GenderId,
                GivenName = userData.GivenName,
                MiddleName = userData.MiddleName,
                PasswordHash = _hasher.HashPassword(userData.Password),
                EmailSubscribeAgree = userData.EmailSubscribeAgree,
                PersonalDataAgree = userData.PersonalDataAgree,
                Source = source
            };
            _context.Users.Add(createdUser);
            _context.SaveChanges();

            return Ok(_responseFactory.CreateSuccessfullyResponse(data.Request.SourceId, new
            {
                userData = new
                {
                    userId = createdUser.Id,
                    secretKey = createdUser.SecretKey
                }
            }));
        } 
    }
}