using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThirdPartyApi.DTOs;

namespace ThirdPartyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Login login, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando");
            await Task.Delay(10000, cancellationToken);
            _logger.LogInformation("Finalizando");
            return Ok();
        }
    }
}
