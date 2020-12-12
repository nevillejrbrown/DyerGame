namespace DyerGame.Models.Service
{
    public interface IGameService
    {
        void CelebGuessed(int celebId);
        Game CreateGame(Game game);
        Celeb GetCeleb(int id);
        Game GetGameById(int Id);
        Game GetGameByCelebId(int CelebId);
        Celeb AddCeleb(Celeb celeb);
        void CelebBurned(int celebId);
    }
}