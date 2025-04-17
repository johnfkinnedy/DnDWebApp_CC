
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DnDWebApp_CC.Models.Entities
{
    /// <summary>
    /// Where a character comes from; what shaped them.
    /// </summary>
    public class Background
    {
        /// <summary>
        /// unique identifier for backgrounds. ignored in json requests
        /// </summary>
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// The name of the background
        /// </summary>
        ///
   
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// The description of the background
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// A list of <see cref="Skill"/>s that the background comes with; usually up to player choice,
        /// player will pick 2 when creating a character
        /// </summary>
        public virtual ICollection<SkillInBackground> Skills { get; set; } = new List<SkillInBackground>();

        /// <summary>
        /// Additional features granted by the background
        /// </summary>
        public virtual ICollection<string> Features { get; set; } = new List<string>();

        /// <summary>
        /// Languages that come with the background. Usually up to player choice;
        /// will be added to the list when creating a character
        /// </summary>
        public virtual ICollection<string> Languages { get; set; } = new List<string>();

        public Background()
        {

        }
    }

    /// <summary>
    /// Associative class for skills used by backgrounds
    /// </summary>
    public class SkillInBackground
    {
        /// <summary>
        /// ID of the associative class
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// ID of the background
        /// </summary>
        [JsonIgnore]
        public int BackgroundId { get; set; }
        /// <summary>
        /// Background, if it exists
        /// </summary>
        [JsonIgnore]
        public Background? Background { get; set; }


        /// <summary>
        /// ID of the skill
        /// </summary>
        [JsonIgnore]
        public int SkillId { get; set; }

        /// <summary>
        /// Sets that the background has proficiency with the skill (bonus to checks for character w/ the background
        /// </summary>
        bool Proficiency { get; set; } = true;
        /// <summary>
        /// The skill, if it exists
        /// </summary>
        public Skill? Skill { get; set; }

        
    }
}
