using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DyerGame.Models.Data;

namespace DyerGame.Models.Service
{
    public class GameService : IGameService
    {

        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context)
        {
            _context = context;
        }

        static class DataRepo
        {
            public static Game game;
            static DataRepo()
            {
                game = new Game();
                game.AddCeleb(new Celeb("Danny Dyer", 67));
                game.AddCeleb(new Celeb("Geoff Capes", 68));
                game.AddCeleb(new Celeb("James Milner", 69));
            }
        }

        public Game GetGame(int Id)
        {
            return DataRepo.game;
        }

        public Game CreateGame(Game game)
        {
            _context.Game.Add(game);
            _context.SaveChanges();
            return game;
        }

        public Celeb GetCeleb(int id)
        {
            return DataRepo.game.GetCelebById(id);
        }

        public void CelebGuessed(int celebId)
        {
            GetCeleb(celebId).Guess();
        }
    }
}
