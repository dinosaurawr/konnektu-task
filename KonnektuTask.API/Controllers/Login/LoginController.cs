using System;
using System.Collections.Generic;
using System.Linq;
using KonnektuTask.API.Factories;
using KonnektuTask.API.Models;
using KonnektuTask.API.Models.Request;
using KonnektuTask.API.Tools;
using KonnektuTask.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace KonnektuTask.API.Controllers.Login
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IPasswordHasher _hasher;
        private readonly ResponseFactory _responseFactory;
        
        public LoginController(ApplicationContext context, IPasswordHasher hasher, ResponseFactory responseFactory)
        {
            _context = context;
            _hasher = hasher;
            _responseFactory = responseFactory;
        }

        [HttpPost]
        public IActionResult Login(BaseRequest<LoginRequest> data)
        {
            var sourceId = Guid.Parse(data.Request.SourceId);
            
            var source = _context.Sources.Find(sourceId);
            if (source == null || source.SecretKey != data.Request.SourceSecretKey)
            {
                ModelState.AddModelError("SourceSecretKey", "Invalid sourceSecretKey");
                return BadRequest(_responseFactory.CreateFailureResponse(data.Request.SourceId, model: ModelState));
            }

            var foundUser = _context.Users.FirstOrDefault(u => u.Email == data.Request.UserData.Email
                                                               && u.Phone == data.Request.UserData.Phone);

            if (foundUser == null || !_hasher.VerifyPasswordHash(foundUser.PasswordHash, data.Request.UserData.Password))
            {
                ModelState.AddModelError("Email or Password", "Wrong email or password");
                return BadRequest(_responseFactory.CreateFailureResponse(data.Request.SourceId, model: ModelState));
            }

            var response = new
            {
                userId = foundUser.Id,
                secretKey = foundUser.SecretKey
            };
            
            return Ok(_responseFactory.CreateSuccessfullyResponse(data.Request.SourceId, new { userData = response}));
        }
    }
}