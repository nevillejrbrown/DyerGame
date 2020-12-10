﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DyerGame.Models;
using Microsoft.Extensions.Logging;
using DyerGame.Models.Service;
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
            return View(_gameService.GetGameById(ID).GetRandomCelebFromHat());
        }

        public IActionResult Guessed(int ID)
        {
            _logger.LogDebug($"Celeb has been guessed: {ID}");

            _gameService.CelebGuessed(ID);

            Game relatedGame = _gameService.GetGameByCelebId(ID);

            if (relatedGame.State == GameState.ROUND_IN_PROGRESS)
            {
                return View("RandomCeleb", relatedGame.GetRandomCelebFromHat());
            }
            else
            {
                return View("FinishedRound");
            }
        }

 

        public IActionResult Next()
        {
            _logger.LogDebug($"Celeb has been chucked back in:");
            return View("RandomCeleb", _gameService.GetGameById(1).GetRandomCelebFromHat());
        }
    }
}
