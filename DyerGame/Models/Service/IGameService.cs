namespace DyerGame.Models.Service
{
    public interface IGameService
    {
        void CelebGuessed(int celebId);
        Game CreateGame();
        Celeb GetCeleb(int id);
        Game GetGame(int Id);
    }
}