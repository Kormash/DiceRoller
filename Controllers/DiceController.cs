using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiceRoller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiceController : ControllerBase
    {

        private readonly ILogger<DiceController> _logger;

        public DiceController(ILogger<DiceController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{diceNumber}/{diceType}")]
        public IEnumerable<Dice> Get(int diceNumber, int diceType)
        {
            diceHelper dh = new();
            var rng = new Random();
            string resultString = dh.rollDice(diceNumber, diceType + 1);
            return Enumerable.Range(1, 1).Select(index => new Dice
            {
                DiceNumber = diceNumber,
                DiceType = diceType,
                ResultString = resultString,
                DiceSum = dh.countSum(resultString)
            })
            .ToArray();
        }
    }

    class diceHelper
    {

        public string rollDice(int diceNumber, int diceType)
        {
            string toReturn = "";
            for (int i = 0; i < diceNumber; i++)
            {
                var rng = new Random();
                if(toReturn == "")
                {
                    toReturn += rng.Next(1, diceType);
                }
                else
                {
                    toReturn += " + " + rng.Next(1, diceType);
                }
                
            }
            return toReturn;
        }

        public int countSum(string ResultString)
        {
            String[] numbers = ResultString.Split(" + ", StringSplitOptions.RemoveEmptyEntries);
            int toReturn = 0;
            foreach(var number in numbers)
            {
                toReturn += int.Parse(number);
            }
            return toReturn;

        }
    }
}