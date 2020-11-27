using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DyerGame.Models
{
    public class GameService
    {

        static class DataRepo
        {
            public static Game game;
            static DataRepo() {
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

        public Game CreateGame()
        {
            return new Game();
        }

        public Celeb GetCeleb(int id)
        {
            return DataRepo.game.GetCeleb(id);
        }

        public void CelebGuessed(int celebId)
        {
            GetCeleb(celebId).Guess();
        }
    }
}
