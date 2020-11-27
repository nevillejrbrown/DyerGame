using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DyerGame.Models
{
    public class Game
    {

        IList<Celeb> Celebs { get; set; }

        private static Random random = new Random();

        public Game()
        {
            this.Celebs = new List<Celeb>();
        }

        public void AddCeleb(Celeb celeb)
        {
            this.Celebs.Add(celeb);
        }

        public GameState State
        {
            get {
                if (Celebs.All(c => c.State == CelebState.IN_HAT)) return GameState.ROUND_READY_TO_START;
                return Celebs.All(c => c.State == CelebState.GUESSED) ? GameState.ROUND_COMPLETE : GameState.ROUND_IN_PROGRESS;
            }

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
