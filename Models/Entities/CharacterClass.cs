using System.ComponentModel.DataAnnotations;

namespace DnDWebApp_CC.Models.Entities
{
    /// <summary>
    /// The class held by a certain character; their 'job', in essence
    /// </summary>
    public class CharacterClass
    {
        [Key]
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
        public List<string> Proficiencies = new List<String>();

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

    public class ClassSpell
    {
        [Key] 
        public int Id { get; set; }

        public int ClassId { get; set; }
        public CharacterClass? CharClass { get; set; }

        public int SpellId { get; set; }
        public Spell? Spell { get; set; }
    }
    public class ClassSkills
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public CharacterClass? CharClass { get; set; }
        
        public int SkillId { get; set; }
        public Skill? Skill { get; set; }
    }
}
