using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DnDWebApp_CC.Models.Entities
{
    [Owned]
    public class Stat
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }
        public List<StatsInCharacter> UsedByCharacters { get; set; } = new List<StatsInCharacter>();


        public Stat(string name)
        {
            this.Name = name;
        }
    }
}
