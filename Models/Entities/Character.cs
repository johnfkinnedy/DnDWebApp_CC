using DnDWebApp_CC.Services;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DnDWebApp_CC.Models.Entities
{
    public class Character
    {
        [JsonIgnore]
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
        public ICollection<string> Features { get; set; } = new List<string>();
        public ICollection<string> Languages { get; set; } = new List<string>();

        //
        public Character(CharacterClass charClass, Background bg, Species species, List<Equipment> equipment)
        {
            this.Background = bg;
            this.Class = charClass;
            this.Species = species;


            //adding spells from the Class if the class is a spellcaster and has spells in its spell list
            if(charClass.Spellcaster == true && !charClass.Spells.IsNullOrEmpty())
            {
                Spells = new List<SpellsInCharacter>();
                var spells = charClass.Spells;
                foreach (ClassSpell s in spells)
                {
                    var characterSpell = new SpellsInCharacter
                    {
                        Spell = s.Spell
                    };
                    Spells.Add(characterSpell);
                }
            }

            foreach(var bgSkill in bg.Skills)
            {
                var characterSkill = new SkillsInCharacter
                {
                    Skill = bgSkill.Skill
                };
            }
            
            foreach(var classSkill in charClass.Skills)
            {
                var characterSkill = new SkillsInCharacter
                {
                    Skill = classSkill.Skill
                };
            }
            //adding features from class, background
            Features.AddRange(bg.Features);
            Features.AddRange(charClass.Features);
            
            //adding equipment
            foreach(var item in equipment)
            {
                var charEquipment = new EquipmentInCharacter
                {
                    Equipment = item
                };
            }
        }

    
        //
    }
    public class EquipmentInCharacter
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int CharacterId { get; set; }
        [JsonIgnore]
        public Character? Character { get; set; }

        [JsonIgnore]
        public int EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }
    }
    public class SpellsInCharacter
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int CharacterId { get; set; }
        [JsonIgnore]
        public Character? Character { get; set; }
        [JsonIgnore]
        public int SpellId { get; set; }
        public Spell? Spell { get; set; }
    }
    public class StatsInCharacter
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int CharacterId { get; set; }
        [JsonIgnore]
        public Character? Character { get; set; }
        [JsonIgnore]

        public int StatId { get; set; }
        public bool Proficiency { get; set; }
        public int Score { get; set; }

        public Stat? Stat { get; set; }
    }
    public class SkillsInCharacter
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int CharacterId { get; set; }
        [JsonIgnore]
        public Character? Character { get; set; }

        [JsonIgnore]
        public int SkillId { get; set; }
        public int Score { get; set; }
        public bool Proficiency { get; set; }
        public Skill? Skill { get; set; }

        public SkillsInCharacter()
        {

            //set skill score based on proficiency and skill's base stat score 
            var characterStat = Character.Stats.FirstOrDefault(s => s.Id == Skill.BaseStat.Id).Score;
            this.Score = Proficiency ? 2 : 0;

        }
    }
}
