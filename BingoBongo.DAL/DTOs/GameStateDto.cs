using BingoBongo.DAL.Models;

namespace BingoBongo.DAL.DTOs
{
    public class GameStateDto
    {
        public GameStateDto(GameState gs)
        {
            Id = gs.Id;
            UserId = gs.UserId;
            DidWon = gs.DidWon;
            Table = ParseNumbersFromString(gs.Table);
            Numbers = ParseNumbersFromString(gs.Numbers);
            NumberOfRolls = gs.NumberOfRolls;
        }

        private static IEnumerable<ushort> ParseNumbersFromString(string text)
        {
            var arr = text.Split(',');
            bool success = Int32.TryParse(arr[0], out int result);
            List<ushort> array = new List<ushort>();
            if (success)
            {
                foreach (string c in arr)
                {
                    array.Add((ushort)Int32.Parse(c));
                }
            }
            return array;
        }

        public int Id { get; set; }
        public bool DidWon { get; set; }
        public int NumberOfRolls { get; set; }
        public IEnumerable<ushort> Table { get; set; }
        public IEnumerable<ushort> Numbers { get; set; }
        public int UserId { get; set; }
    }
}
