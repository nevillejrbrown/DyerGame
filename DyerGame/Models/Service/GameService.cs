using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DyerGame.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace DyerGame.Models.Service
{
    public class GameService : IGameService
    {

        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context)
        {
            _context = context;
        }


        public Game GetGameById(int Id)
        {
            //_context.Game.;
            return _context.Game.Include(game => game.Celebs)
                                .Single(g => g.Id == Id);
        }

        public Game CreateGame(Game game)
        {
            _context.Game.Add(game);
            _context.SaveChanges();
            return game;
        }

        public Game GetGameByCelebId(int CelebId)
        {
            Celeb celeb = _context.Celeb.Single(c => c.Id == CelebId);
            return _context.Game.Include(g => g.Celebs)
                                .Single(g => g.Id == celeb.GameId);
        }


        public void CelebGuessed(int celebId)
        {
            Celeb celeb = _context.Celeb.Single(c => c.Id == celebId);
            celeb.Guess();
            _context.Celeb.Update(celeb);
            _context.SaveChanges();
        }

        public void CelebBurned(int celebId)
        {
            Celeb celeb = _context.Celeb.Single(c => c.Id == celebId);
            celeb.Burn();
            _context.Celeb.Update(celeb);
            _context.SaveChanges();
        }

        public Celeb AddCeleb(Celeb celeb)
        {
            _context.Celeb.Add(celeb);
            _context.SaveChanges();
            return celeb;
        }

        Celeb IGameService.GetCeleb(int id)
        {
            throw new NotImplementedException();
        }
    }
}
