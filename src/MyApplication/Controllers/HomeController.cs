using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyApplication.Data;
using MyApplication.Entities;
using MyApplication.Models;
using Newtonsoft.Json;

namespace MyApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dataContext;

        public HomeController(ILogger<HomeController> logger,
                              DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RegisterWithoutTransaction(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando");
            var userName = $"User-{Guid.NewGuid()}";
            _dataContext.Logins.Add(new Login(userName));
            await _dataContext.SaveChangesAsync(cancellationToken);

            await Task.Delay(5000, cancellationToken);

            var statistics = _dataContext.Statistics.FirstOrDefault();
            if (statistics is null)
            {
                statistics = new Statistics();
                _dataContext.Add(statistics);
            }
            statistics.Accounts++;
            await _dataContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finalizando");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RegisterWithUnitOfWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando");
            var userName = $"User-{Guid.NewGuid()}";
            _dataContext.Logins.Add(new Login(userName));

            await Task.Delay(5000, cancellationToken);

            var statistics = _dataContext.Statistics.FirstOrDefault();
            if (statistics is null)
            {
                statistics = new Statistics();
                _dataContext.Add(statistics);
            }
            statistics.Accounts++;

            await _dataContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finalizando");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RegisterWithThirdPartyApi(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando");
            var userName = $"User-{Guid.NewGuid()}";
            var login = new Login(userName);

            var httpClient = new HttpClient();
            string json = JsonConvert.SerializeObject(login);
            var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await httpClient.PostAsync("https://localhost:5003/api/Login", httpContent, cancellationToken);

            _dataContext.Logins.Add(login);

            var statistics = _dataContext.Statistics.FirstOrDefault();
            if (statistics is null)
            {
                statistics = new Statistics();
                _dataContext.Add(statistics);
            }
            statistics.Accounts++;

            await _dataContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finalizando");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RegisterWithThirdPartyApi2(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando");
            var userName = $"User-{Guid.NewGuid()}";
            var login = new Login(userName);

            var httpClient = new HttpClient();
            string json = JsonConvert.SerializeObject(login);
            var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await httpClient.PostAsync("https://localhost:5003/api/Login2", httpContent, cancellationToken);

            _dataContext.Logins.Add(login);

            var statistics = _dataContext.Statistics.FirstOrDefault();
            if (statistics is null)
            {
                statistics = new Statistics();
                _dataContext.Add(statistics);
            }
            statistics.Accounts++;

            await _dataContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finalizando");

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
