using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogProject.Business.Tools.FacadeTool;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly IFacade _facade;
        public ErrorController(IFacade facade)
        {
            _facade = facade;
        }
        [HttpGet]
        public IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _facade.CustomLogger.LogError($"\nWhere the error occurred : {errorInfo.Path}\n Error Message : {errorInfo.Error.Message}\n Stack Trace : {errorInfo.Error.StackTrace}");
            return Problem(detail: "Something went wrong. It will be fixed as soon as possible.");
        }
    }
}
