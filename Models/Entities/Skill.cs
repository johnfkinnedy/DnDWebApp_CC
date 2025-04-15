using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace DnDWebApp_CC.Models.Entities
{
    [Owned]
    /// <summary>
    /// Represents a skill that a <see cref="Character"/> has.
    /// </summary>
    public class Skill 
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// The name of the skill
        /// </summary>
        public string Name { get; set; }  = string.Empty;  
        
        /// <summary>
        /// The description of the skill; what it does
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Set to true if the player with this skill has proficiency (a bonus) with it.
        /// Defaults to false.
        /// </summary>
        public bool Proficiency { get; set; } = false;

        /// <summary>
        /// The stat that this skill corresponds to.
        /// </summary>
        public Stat BaseStat { get; set; } 
        /// <summary>
        /// The base score for the skill; directly corresponds to the bonus amount.
        /// Set by the corresponding Stat
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// A positive or negative integer referring to how good/bad at the skill a character is.
        /// </summary>
        public int Bonus { get; set; } = 0;
        
        public ICollection<SkillInBackground> UsedByBackgrounds { get; set; } = new List<SkillInBackground>();
        public ICollection<SkillsInCharacter> UsedByCharacters { get; set; } = new List<SkillsInCharacter>();
        public ICollection<SkillsInSpecies> UsedBySpecies { get; set; } = new List<SkillsInSpecies>();
        public ICollection<ClassSkills> UsedByClasses { get; set; } = new List<ClassSkills>();



    }
}
