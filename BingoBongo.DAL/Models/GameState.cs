namespace BingoBongo.DAL.Models
{
    public class GameState
    {

        public int Id { get; set; }
        public bool DidWon { get; set; }
        public int NumberOfRolls { get; set; }
        public string Table { get; set; }
        public string Numbers { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
