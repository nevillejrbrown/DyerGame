using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
namespace DyerGame.Models
{
    public enum GameState
    {
        ROUND_READY_TO_START,
        ROUND_IN_PROGRESS,
        ROUND_COMPLETE
    }

    public enum GameRound
    {
        DESCRIBE,
        DESCRIBE_3_WORDS,
        MIME,
        GAME_OVER
    }

    public class Game
    {
        public int GameId { get; set; }
        public IList<Celeb> Celebs { get; set; }

        private static Random random = new Random();

        public GameRound Round { get; set; }
 

        public void NextRound()  // TODO ADD TESTS
        {
            if (Round < GameRound.GAME_OVER)
            {
                Round++;
            }
            else throw new InvalidOperationException("Game is already over");
        }

        public Game()
        {
            this.Celebs = new List<Celeb>();
            Round = GameRound.DESCRIBE;
        }

        public void AddCeleb(Celeb celeb)
        {
            this.Celebs.Add(celeb);
        }

        [NotMapped]
        public GameState State
        {
            get {
                if (Celebs.All(c => c.State == CelebState.IN_HAT)) return GameState.ROUND_READY_TO_START;
                return Celebs.All(c => c.State == CelebState.GUESSED) ? GameState.ROUND_COMPLETE : GameState.ROUND_IN_PROGRESS;
            }

        }

        public Celeb GetCeleb(Celeb celeb)
        {
            return Celebs.Where(c => celeb.Equals(c)).Single();
        }

        public Celeb GetCeleb(int id)
        {
            return Celebs.Where(c => c.CelebId == id).Single();
        }

        public IEnumerable<Celeb> CelebsInHat
        {
            get => Celebs.Where(c => c.State == CelebState.IN_HAT);
        }

        public IEnumerable<Celeb> GuessedCelebs
        {
            get => Celebs.Where(c => c.State == CelebState.GUESSED);
        }

        public Celeb GetRandomCelebFromHat()
        {
            return CelebsInHat.GetRandom<Celeb>(random) ;
        }

        public void PutAllGuessedCelebsBackInHat()
        {
            foreach (Celeb celeb in GuessedCelebs)
            {
                celeb.PutBackIntoHat();
            }
        }

    }

    static class RandomizerExtensions
    {
        public static Celeb GetRandom<T>(this IEnumerable<Celeb> enumerable, Random random)
        {
            return enumerable.Count() > 0 ?
                enumerable.ElementAt(random.Next(0, enumerable.Count())) :
                null;
        }
    }
}
