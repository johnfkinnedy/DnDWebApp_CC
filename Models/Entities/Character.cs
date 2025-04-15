using DnDWebApp_CC.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDWebApp_CC.Models.Entities
{
    public class Character
    {
        [Key]
        public int id { get; set; }

        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Alignment { get; set; } = string.Empty;

        public CharacterClass Class { get; set; }
        public CharacterClass? SecondClass { get; set; }
        public Species Species { get; set; }
        public Background Background { get; set; }
        public ICollection<EquipmentInCharacter> Equipment { get; set; } = new List<EquipmentInCharacter>();
        public ICollection<SpellsInCharacter>? Spells { get; set; }
        public ICollection<StatsInCharacter> Stats { get; set; } = new List<StatsInCharacter>();
        public int ProficiencyBonus { get; set; }
        public int Level { get; set; }
        public ICollection<SkillsInCharacter> Skills { get; set; } = new List<SkillsInCharacter>();
        public ICollection<string> Featues { get; set; } = new List<string>();
        public ICollection<string> Languages { get; set; } = new List<string>();

    }
    public class EquipmentInCharacter
    {
        public int Id { get; set; }

        public int CharacterId { get; set; }
        public Character? Character { get; set; }

        public int EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }
    }
    public class SpellsInCharacter
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public Character? Character { get; set; }

        public int SpellId { get; set; }
        public Spell? Spell { get; set; }
    }
    public class StatsInCharacter
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public Character? Character { get; set; }

        public int StatId { get; set; }
        public Stat? Stat { get; set; }
    }
    public class SkillsInCharacter
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public Character? Character { get; set; }

        public int SkillId { get; set; }
        public Skill? Skill { get; set; }
    }
}
