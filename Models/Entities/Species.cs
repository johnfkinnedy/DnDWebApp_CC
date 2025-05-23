﻿using System.ComponentModel.DataAnnotations;
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

    /// <summary>
    /// Associative class for skills used by Species
    /// </summary>
    public class SkillsInSpecies
    {
        /// <summary>
        /// Unique ID for association
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// The ID of the species
        /// </summary>
        [JsonIgnore]
        public int SpeciesId { get; set; }
        /// <summary>
        /// Species object
        /// </summary>
        [JsonIgnore]
        public Species? Species { get; set; }
        
        /// <summary>
        /// The ID of the skill
        /// </summary>
        [JsonIgnore]
        public int SkillId {  get; set; }  
        /// <summary>
        /// Skill object
        /// </summary>
        public Skill? Skill { get; set; }  
    }
}
