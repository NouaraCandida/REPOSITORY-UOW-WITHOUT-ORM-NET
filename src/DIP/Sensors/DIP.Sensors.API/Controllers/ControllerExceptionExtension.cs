using System;
using DIP.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DIP.Sensors.API.Controllers
{
    public static class ControllerExceptionExtension
    {
        public static ActionResult CreateExceptionExtension(this ControllerBase controller, Exception ex)
        {
            return ex switch
            {
                ArgumentOutOfRangeException e => controller.BadRequest(e.Message),
                ValidateException e => controller.BadRequest(e.Message),

                NotFoundException e => controller.NotFound(e.Message),

                _ => controller.StatusCode(StatusCodes.Status500InternalServerError, ex.Message),
            };
        }
    }
}
