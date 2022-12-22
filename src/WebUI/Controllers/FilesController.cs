using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SynopsisV2.Application.Errors.Commands.CreateError;
using WebUI.Services;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ApiControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    
    public FilesController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    // GET

    [HttpPost("[action]")]
    public async Task<ActionResult> UploadFile([FromQuery] string f, CancellationToken token = default)
    {
        string ret = "";
        try
        {
            foreach (IFormFile file in Request.Form.Files)
            {
                
                string fName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().ToString()
                    .ToLower();
                if (file.Length > 0 &&
                    (fName.EndsWith(".png") || fName.EndsWith(".jpg") || fName.EndsWith(".jpeg") ||
                     fName.EndsWith(".gif")) && file.Length < 20 * 1024 * 1024)
                {
                    // get a stream
                    
                    // and optionally write the file to disk
                    var filePath = _webHostEnvironment.WebRootPath + ("/images/" + f + "/");

                    string fileName = mainFuncs.GenerateCode(25) +
                                      fName.Substring(fName.IndexOf("."), fName.Length - fName.IndexOf(".")); //todo Create Utills mainFyuncs
                    string pathTemp = filePath + fileName;

                    await using (var fileStream = new FileStream(pathTemp, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream, token);
                    }

                    ret = fileName;
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Source != null)
                await Mediator.Send(
                    new CreateErrorCommand(ex.Message, ControllerContext.ActionDescriptor.ControllerName,
                        ControllerContext.ActionDescriptor.ActionName,
                        ex.Source),
                    token);
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Ok("Upload failed");
        }

        return Ok(ret);
    }
}