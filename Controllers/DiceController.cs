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
            return Enumerable.Range(1, 1).Select(index => new Dice
            {
                DiceNumber = diceNumber,
                DiceType = diceType,
                Result = dh.rollDice(diceNumber, diceType+1)
            })
            .ToArray();
        }



    }

    class diceHelper
    {

        public int rollDice(int diceNumber, int diceType)
        {
            int toReturn = 0;
            for (int i = 0; i < diceNumber; i++)
            {
                var rng = new Random();
                toReturn += rng.Next(1, diceType);
            }
            return toReturn;
        }

    }
}