using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Christmas.Models;

namespace Christmas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Christmas()
        {
            return View();
        }

        public IActionResult Results()
        {
            ViewData["Results"] = Math.Round(TwoThirdAverageGame.GetTwoThirdOfAverage(), 2);
            ViewData["Winner"] = TwoThirdAverageGame.GetWinner();
            ViewData["Count"] = TwoThirdAverageGame.GetNumberOfSubmissions();
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
