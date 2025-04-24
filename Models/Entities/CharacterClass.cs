using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DnDWebApp_CC.Models.Entities
{
    /// <summary>
    /// The class held by a certain character; their 'job', in essence
    /// </summary>
    public class CharacterClass
    {
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// The name of the class
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the class; what its basic functions are.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// A list of things that the character is proficient at
        /// </summary>
        public List<string> Proficiencies { get; set; } = new List<String>();

        /// <summary>
        /// The dice size that determines your pool of hit points. Just a size denomination
        /// </summary>
        public Dice HitDice { get; set; }

        /// <summary>
        /// A collection of <see cref="Skill"/>s.
        /// </summary>
        public ICollection<ClassSkills> Skills { get; set; } = new List<ClassSkills>();

        /// <summary>
        /// Flags if the class is able to cast spells or not.
        /// </summary>
        public bool Spellcaster { get; set; }

        /// <summary>
        /// An optional collection of <see cref="Spell"/>s. Only used by some classes
        /// </summary>
        public List<ClassSpell>? Spells { get; set; }

        /// <summary>
        /// Any other features that are given to the class. 
        /// </summary>
        public List<string> Features { get; set; } = new List<string>();
    }

    /// <summary>
    /// Associative class for spells used by classes
    /// </summary>
    public class ClassSpell
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int ClassId { get; set; }
        [JsonIgnore]
        public CharacterClass? CharClass { get; set; }

        [JsonIgnore]
        public int SpellId { get; set; }
        public Spell? Spell { get; set; }
    }
    public class ClassSkills
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int ClassId { get; set; }
        [JsonIgnore]
        public CharacterClass? CharClass { get; set; }

        [JsonIgnore]
        public int SkillId { get; set; }
        bool Proficiency { get; set; } = true;
        public Skill? Skill { get; set; }
       
    }
}
