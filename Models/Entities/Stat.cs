using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DnDWebApp_CC.Models.Entities
{
    [Owned]
    public class Stat
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;


        public Stat(string name)
        {
            this.Name = name;
        }
    }
}
