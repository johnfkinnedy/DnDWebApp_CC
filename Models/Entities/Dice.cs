using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DnDWebApp_CC.Models.Entities
{
    public class Dice
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Size { get; set; } = string.Empty;
        public int NumberToRoll { get; set; }
        public ICollection<DiceInSpells> UsedInSpells = new List<DiceInSpells>();

        public Dice(string size)
        {
            this.Size = size;
        }
    }
    public class DiceInSpells
    {
        public int Id { get; set; }
        public int DiceId { get; set; }
        public Dice? Dice { get; set; }

        public int SpellId {  get; set; }   
        public Spell? Spell { get; set; }
    }
}
