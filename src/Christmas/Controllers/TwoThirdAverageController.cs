using System;
using Microsoft.AspNetCore.Mvc;
using Christmas.Models;

namespace Christmas.Controllers
{
    [Route("[controller]")]
    public class TwoThirdAverageController : Controller
    {
        [HttpGet("Count")]
        public int Count()
        {
            return TwoThirdAverageGame.GetNumberOfSubmissions();
        }

        [HttpGet("Result")]
        public double Result()
        {
            return Math.Round(TwoThirdAverageGame.GetTwoThirdOfAverage(), 8);
        }

        [HttpGet("Winner")]
        public string Winner()
        {
            return TwoThirdAverageGame.GetWinner();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Submission value)
        {
            if (value != null)
                TwoThirdAverageGame.Submit(value.Name, value.Number);
        }

        [HttpPost("Reset")]
        public void Reset()
        {
            TwoThirdAverageGame.Reset();
        }
    }
}
