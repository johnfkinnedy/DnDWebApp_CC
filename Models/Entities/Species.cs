using System.ComponentModel.DataAnnotations;

namespace DnDWebApp_CC.Models.Entities
{
    public class Species
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// The name of the species
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the species
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Ability score increases given by the skill
        /// </summary>
        public ICollection<SkillsInSpecies> Skills { get; set; } = new List<SkillsInSpecies>();

        /// <summary>
        /// Languages that come with the species. These just come with your character.
        /// </summary>
        public List<string> Languages { get; set; } = new List<string>();
    }

    public class SkillsInSpecies
    {
        public int Id { get; set; }
        public int SpeciesId { get; set; }
        public Species? Species { get; set; }

        public int SkillId {  get; set; }  
        public Skill? Skill { get; set; }  
    }
}
