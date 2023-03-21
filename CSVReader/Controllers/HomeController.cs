using Contracts;
using CSVReader.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CSVReader.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly ICsvService _service;
        public HomeController(ILogger<HomeController> logger, ICsvService service) {
            _logger = logger;
            _service = service; 
        }

        public IActionResult Index() {
            var answer = _service.OpenFile("c:\\1\\test.csv", true);
            return View(answer);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}