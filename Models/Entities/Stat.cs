using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DnDWebApp_CC.Models.Entities
{
    /// <summary>
    /// A basic ability of a character; Strength, Dexterity, Intelligence, Wisdom, Charisma, and Constitution.
    /// </summary>
    [Owned]
    public class Stat
    {
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// The name of the stat
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Constructor for a stat
        /// </summary>
        /// <param name="name">The name to give the stat</param>

        public Stat(string name)
        {
            this.Name = name;
        }
    }
}
