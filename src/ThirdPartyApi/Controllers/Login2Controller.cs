using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThirdPartyApi.DTOs;

namespace ThirdPartyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Login2Controller : ControllerBase
    {
        private readonly ILogger<Login2Controller> _logger;

        public Login2Controller(ILogger<Login2Controller> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Login login)
        {
            _logger.LogInformation("Iniciando");
            await Task.Delay(10000);
            _logger.LogInformation("Finalizando");
            return Ok();
        }
    }
}
