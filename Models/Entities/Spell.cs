using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DnDWebApp_CC.Models.Entities
{
    /// <summary>
    /// A magical ability that is cast by some classes
    /// </summary>
    public class Spell
    {
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// The name of the spell
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the spell; what it does
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Optional damage that the spell deals, in an <see cref="int"/> amount of <see cref="Dice"/>
        /// </summary>
        public Dice? DiceDenomination { get; set; }
        /// <summary>
        /// The dice amount to roll (redundant)
        /// </summary>
        public int? DiceToRoll { get; set; }

        /// <summary>
        /// Association list; Dice used by the spells (i dont think i need this)
        /// </summary>
        public ICollection<DiceInSpells> UsedInSpells = new List<DiceInSpells>();


        /// <summary>
        /// The spell slot level used by this spell.
        /// 0 is a cantrip, which doesn't have a slot cost.
        /// Anything above 0 typically costs a spell slot of that level to cast.
        /// </summary>
        public int SlotLevel { get; set; }

    }
}
