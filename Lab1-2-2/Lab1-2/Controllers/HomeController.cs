using Lab1_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public string About()
        {
            return "Informacja o stronie";
        }

        public string Hello(string name, int age)
        {
            return $"Witaj {name}, masz {age} lat";
        }

        /* Napisz akcje Powe, która podnosi do kwadratu parametr z query */

        public string Power(decimal? num) // ? - może wystąpić wartość null
        {
            if (num == null)
            {
                return $"Witaj, nie mogę policzyć, brak parametru lub niepoprawna nazwa";
            }
                return $"Witaj, twoja liczba {num} do kwadratu to {num * num}";
        }



        public IActionResult PowerForm()
        {
            return View();
        }

        public IActionResult Waluty()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
