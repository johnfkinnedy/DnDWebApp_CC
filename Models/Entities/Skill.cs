using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json.Serialization;

namespace DnDWebApp_CC.Models.Entities
{
    [Owned]
    /// <summary>
    /// Represents a skill that a <see cref="Character"/> has.
    /// </summary>
    public class Skill
    {
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// The name of the skill
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the skill; what it does
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The stat that this skill corresponds to.
        /// </summary>
        public Stat BaseStat { get; set; }

       
    }
    
}
