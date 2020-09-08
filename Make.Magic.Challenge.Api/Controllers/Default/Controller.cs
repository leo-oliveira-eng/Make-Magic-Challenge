using Messages.Core;
using Messages.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.Api.Controllers.Default
{
    public abstract class Controller : ControllerBase
    {
        protected async Task<IActionResult> WithResponseAsync<TResponseMessage>(Func<Task<Response<TResponseMessage>>> func)
        {
            var response = await func.Invoke();

            if (!response.HasError)
                return Ok(response);

            if (response.Messages.Any(message => message.Type == MessageType.BusinessError))
                return BadRequest(response);

            return StatusCode(500, response);
        }
    }
}
