using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KonnektuTask.API.Factories;
using KonnektuTask.API.Models;
using KonnektuTask.API.Models.Request;
using KonnektuTask.Core;
using KonnektuTask.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using File = KonnektuTask.Core.File;

namespace KonnektuTask.API.Controllers.Files
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly ResponseFactory _responseFactory;
        
        public FileController(ApplicationContext context, ResponseFactory responseFactory)
        {
            _context = context;
            _responseFactory = responseFactory;
        }

        [HttpPost("/api/uploadfile")]
        public IActionResult UploadFile([FromBody] BaseRequest<UploadFileRequest> data)
        {
            if (!Guid.TryParse(data.Request.SourceId, out var sourceId))
                return BadRequest(_responseFactory.CreateFailureResponse(data.Request.SourceId));
            
            var source = _context.Sources.Find(sourceId);
            if (source == null || source.SecretKey != data.Request.SourceSecretKey)
                return BadRequest(_responseFactory.CreateFailureResponse(data.Request.SourceId));
            
            if (data.Request == null)
                return BadRequest(_responseFactory.CreateFailureResponse(data.Request.SourceId));

            if (data.Request.Files.ToArray().Length == 0)
                return BadRequest(_responseFactory.CreateFailureResponse(data.Request.SourceId));

            var target = Path.Join(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(target);

            var createdFiles = new List<File>();
            foreach (var fileData in data.Request.Files)
            {
                System.IO.File.WriteAllBytes(Path.Join(target, fileData.Name), 
                    Convert.FromBase64String(fileData.ContentByte));
                createdFiles.Add(new File()
                {
                    Name = fileData.Name,
                    Path = Path.Join(target, fileData.Name)
                });
            }
            
            _context.Files.AddRange(createdFiles);
            _context.SaveChanges();
            
            var uploadFilesDto = new List<UploadFileDto>();
            foreach (var file in createdFiles)
            {
                uploadFilesDto.Add(new UploadFileDto()
                {
                    Content = file.Content,
                    Id = file.Id.ToString(),
                    Name = file.Name
                });
            }

            return Ok(_responseFactory.CreateSuccessfullyResponse(data.Request.SourceId, new { files = uploadFilesDto}));
        }
    }
}