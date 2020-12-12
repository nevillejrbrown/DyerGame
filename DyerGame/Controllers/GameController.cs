using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DyerGame.Models;
using Microsoft.Extensions.Logging;
using DyerGame.Models.Service;
using DyerGame.Views;

namespace DyerGame.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;
        private readonly IGameService _gameService;
        public GameController(ILogger<GameController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult RandomCeleb(int ID)
        {
            _logger.LogDebug($"Random celeb requested for ID {ID}");
            Game game = _gameService.GetGameById(ID);
            var model = new CelebAndGamePageModel
            {
                ThisGame = game,
                Celeb = game.GetRandomCelebFromHat()
            };
            return View(model);
        }

        public IActionResult Guessed(int ID)
        {
            _logger.LogDebug($"Celeb has been guessed: {ID}");

            _gameService.CelebGuessed(ID);

            Game relatedGame = _gameService.GetGameByCelebId(ID);

            if (relatedGame.State == GameState.ROUND_IN_PROGRESS)
            {
                var model = new CelebAndGamePageModel
                {
                    ThisGame = relatedGame,
                    Celeb = relatedGame.GetRandomCelebFromHat()
                };
                return View("RandomCeleb", model);
            }
            else
            {
                return View("FinishedRound");
            }
        }

 

        public IActionResult Next(int ID)
        {
            return RandomCeleb(ID);
        }


        public IActionResult Burn(int ID)
        {
            _logger.LogDebug($"Celeb has been burned: {ID}");

            _gameService.CelebBurned(ID);

            Game relatedGame = _gameService.GetGameByCelebId(ID);

            if (relatedGame.State == GameState.ROUND_IN_PROGRESS)
            {
                var model = new CelebAndGamePageModel
                {
                    ThisGame = relatedGame,
                    Celeb = relatedGame.GetRandomCelebFromHat()
                };
                return View("RandomCeleb", model);
            }
            else
            {
                return View("FinishedRound");
            }
        }
    }
}
