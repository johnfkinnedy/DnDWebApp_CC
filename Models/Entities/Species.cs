using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DnDWebApp_CC.Models.Entities
{
    public class Species
    {
        [JsonIgnore]
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
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int SpeciesId { get; set; }
        [JsonIgnore]
        public Species? Species { get; set; }

        [JsonIgnore]
        public int SkillId {  get; set; }  
        public Skill? Skill { get; set; }  
    }
}
