using Make.Magic.Challenge.ApplicationService.Services.Contracts;
using Make.Magic.Challenge.Messages.RequestMessages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Controller = Make.Magic.Challenge.Api.Controllers.Default.Controller;

namespace Make.Magic.Challenge.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CharacterController : Controller
    {
        ICharacterApplicationService CharacterApplicationService { get; }

        public CharacterController(ICharacterApplicationService characterApplicationService)
        {
            CharacterApplicationService = characterApplicationService ?? throw new ArgumentNullException(nameof(characterApplicationService));
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CreateCharacterAsync([FromBody] CharacterRequestMessage requestMessage)
            => await WithResponseAsync(() => CharacterApplicationService.CreateAsync(requestMessage));

        [HttpGet, Route("{code}")]
        public async Task<IActionResult> GetCharacterAsync(Guid code)
            => await WithResponseAsync(() => CharacterApplicationService.GetCharacterAsync(code));

        [HttpGet, Route("")]
        public async Task<IActionResult> GetCharacterAsync([FromQuery] GetCharactersRequestMessage requestMessage)
            => await WithResponseAsync(() => CharacterApplicationService.GetCharacterAsync(requestMessage));

        [HttpPut, Route("{code}")]
        public async Task<IActionResult> UpdateCharacterAsync(Guid code, [FromBody] CharacterRequestMessage requestMessage)
            => await WithResponseAsync(() => CharacterApplicationService.UpdateAsync(code, requestMessage));

        [HttpDelete, Route("{code}")]
        public async Task<IActionResult> DeleteCharacterAsync(Guid code)
            => await WithResponseAsync(() => CharacterApplicationService.RemoveAsync(code));
    }
}
