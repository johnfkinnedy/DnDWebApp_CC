
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

    public class SkillInBackground
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int BackgroundId { get; set; }
        [JsonIgnore]
        public Background? Background { get; set; }

        [JsonIgnore]
        public int SkillId { get; set; }
        bool Proficiency { get; set; } = true;
        public Skill? Skill { get; set; }

        
    }
}
