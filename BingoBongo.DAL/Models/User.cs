namespace BingoBongo.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? GameStateId { get; set; }
        public GameState GameState { get; set; }
    }
}
