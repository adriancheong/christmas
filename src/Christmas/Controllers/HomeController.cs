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
            return RedirectToAction("Game");
            //return View();
        }

        public IActionResult Game()
        {
            return View();
        }

        public IActionResult Results()
        {
            ViewData["Results"] = Math.Round(TwoThirdAverageGame.GetTwoThirdOfAverage(), 2);
            ViewData["Winner"] = TwoThirdAverageGame.GetWinner();
            ViewData["Second"] = TwoThirdAverageGame.GetSecond();
            ViewData["Third"] = TwoThirdAverageGame.GetThird();
            ViewData["Count"] = TwoThirdAverageGame.GetNumberOfSubmissions();
            return View();
        }

        public IActionResult AD()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult LuckyDraw()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
