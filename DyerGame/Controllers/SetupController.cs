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
    public class SetupController : Controller
    {

        private readonly ILogger<GameController> _logger;
        private readonly IGameService _gameService;
        public SetupController(ILogger<GameController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNew([Bind("Name")] Game game)
        {
            _logger.LogDebug("New game created");
            _gameService.CreateGame(game);

            return View("ManageGame", game);
        }

        public IActionResult ManageGame(int ID)
        {
            return View("ManageGame", _gameService.GetGameById(ID));
        }

        public IActionResult New()
        {
            _logger.LogDebug("New game selected");
            return View("NewGame");
        }

        public IActionResult EnterCelebs(int ID)
        {
            _logger.LogDebug($"Going to add celebs page for game {ID}");
            Game game = _gameService.GetGameById(ID);
            CelebAndGamePageModel pageModel = new CelebAndGamePageModel
            {
                ThisGame = game,
                Celeb = new Celeb()
            };
            return View("EnterCeleb", pageModel);
        }

        public IActionResult AddNewCeleb(CelebAndGamePageModel pageModel)
        {
            _logger.LogDebug($"Adding celeb {pageModel.Celeb.Name} to game {pageModel.Celeb.GameId}");
            var savedCeleb = _gameService.AddCeleb(pageModel.Celeb);

            CelebAndGamePageModel newPageModel = new CelebAndGamePageModel
            {
                ThisGame = _gameService.GetGameById(savedCeleb.GameId),
                Celeb = new Celeb()
            };

            return View("EnterCeleb", newPageModel);
        }

        public IActionResult PlayGame(Game game)
        {
            return null;

        }
    }
}
