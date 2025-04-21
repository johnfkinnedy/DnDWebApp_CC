using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DnDWebApp_CC.Models.Entities
{
    /// <summary>
    /// A model of a physical dice to be rolled
    /// </summary>
    public class Dice
    {
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// The size denomination of the dice: d4, d6, d8, etc.
        /// </summary>
        public string Size { get; set; } = string.Empty;

        /// <summary>
        /// The number of dice to roll
        /// </summary>
        public int NumberToRoll { get; set; }

       /// <summary>
       /// Constructor for dice
       /// </summary>
       /// <param name="size">The size of the <see cref="Dice"/></param>
       /// <param name="numToRoll">The number of <see cref="Dice"/> to roll</param>
        public Dice(string size, int numToRoll)
        {
            this.Size = size;
            this.NumberToRoll = numToRoll;
        }
    }
    /// <summary>
    /// Associative class for dice used by spells
    /// </summary>
    public class DiceInSpells
    {
        /// <summary>
        /// Unique ID for association
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The ID of the dice to be associated
        /// </summary>
        public int DiceId { get; set; }
        /// <summary>
        /// The dice object
        /// </summary>
        public Dice? Dice { get; set; }

        /// <summary>
        /// The ID of the spell to be associated
        /// </summary>
        public int SpellId {  get; set; }   
        /// <summary>
        /// The spell object
        /// </summary>
        public Spell? Spell { get; set; }
    }
}
