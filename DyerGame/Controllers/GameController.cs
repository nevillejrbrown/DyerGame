using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DyerGame.Models;
using Microsoft.Extensions.Logging;

namespace DyerGame.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public GameController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

    public IActionResult Index()
        {
            return View();
        }

        public IActionResult RandomCeleb(int ID)
        {
            _logger.LogDebug($"Random celeb requested for ID {ID}");
            return View(new GameService().GetGame(ID).GetRandomCelebFromHat());
        }

        public IActionResult Guessed(int ID)
        {
            _logger.LogDebug($"Celeb has been guessed: {ID}");
            var gameService = new GameService();
            gameService.CelebGuessed(ID);

            if (gameService.GetGame(1).State == GameState.ROUND_IN_PROGRESS) 
            {
                return View("RandomCeleb", gameService.GetGame(1).GetRandomCelebFromHat());
            }
            else
            {
                return View("FinishedRound");
            }
        }

        public IActionResult Next()
        {
            _logger.LogDebug($"Celeb has been guessed:");
            return View("RandomCeleb", new GameService().GetGame(1).GetRandomCelebFromHat());
        }
    }
}
