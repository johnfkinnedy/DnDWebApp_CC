using DnDWebApp_CC.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DnDWebApp_CC.Models.Entities
{
    /// <summary>
    /// Class for a player's character. holds all data about the character
    /// </summary>
    public class Character
    {
        /// <summary>
        /// the unique ID of the character
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// Name of the character
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional age for the character
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// The moral alignment of the character
        /// </summary>
        public string Alignment { get; set; } = string.Empty;

        /// <summary>
        /// The <see cref="CharacterClass"/> of the Character
        /// </summary>
        public CharacterClass Class { get; set; }

        /// <summary>
        /// An optional second <see cref="CharacterClass"/> for the character
        /// </summary>
        public CharacterClass? SecondClass { get; set; }
        
        /// <summary>
        /// The <see cref="Species"/> of the character
        /// </summary>
        public Species Species { get; set; }

        /// <summary>
        /// The <see cref="Background"/> of the character
        /// </summary>
        public Background Background { get; set; }

        /// <summary>
        /// A list of <see cref="Equipment"> that the character has; uses associtive class <see cref="EquipmentInCharacter"/>
        /// </summary>
        public ICollection<EquipmentInCharacter> Equipment { get; set; } = new List<EquipmentInCharacter>();

        /// <summary>
        /// An optional list of <see cref="Spell"/>s for the character; uses associative class <see cref="SpellsInCharacter"/>
        /// </summary>
        public ICollection<SpellsInCharacter>? Spells { get; set; }

        /// <summary>
        /// Stats that the character has. Uses associative class <see cref="StatsInCharacter"/>
        /// </summary>
        public ICollection<StatsInCharacter> Stats { get; set; } = new List<StatsInCharacter>();

        /// <summary>
        /// The character's overall proficiency bonus
        /// </summary>
        public int ProficiencyBonus { get; set; }

        /// <summary>
        /// The overall level of the character
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The skills that the character has; uses associative class <see cref="SkillsInCharacter"/>
        /// </summary>
        public ICollection<SkillsInCharacter> Skills { get; set; } = new List<SkillsInCharacter>();

        /// <summary>
        /// A list of features that the character has
        /// </summary>
        public ICollection<string> Features { get; set; } = new List<string>();

        /// <summary>
        /// A list of languages that the character knows
        /// </summary>
        public ICollection<string> Languages { get; set; } = new List<string>();

        /*
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

    
        */
    }
    /// <summary>
    /// Associative class for <see cref="Equipment"/> used by <see cref="Character"/>s
    /// </summary>
    public class EquipmentInCharacter
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// ID of the character
        /// </summary>
        [JsonIgnore]
        public int CharacterId { get; set; }

        /// <summary>
        /// The character object
        /// </summary>
        [JsonIgnore]
        public Character? Character { get; set; }

        /// <summary>
        /// ID of the equipment
        /// </summary>
        [JsonIgnore]
        public int EquipmentId { get; set; }

        /// <summary>
        /// Equipment object
        /// </summary>
        public Equipment? Equipment { get; set; }
    }

    /// <summary>
    /// Associative class for <see cref="Spell"/>s used by a <see cref=""/>Character
    /// </summary>
    public class SpellsInCharacter
    {

        /// <summary>
        /// Unique identifier
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// ID of the Character
        /// </summary>
        [JsonIgnore]
        public int CharacterId { get; set; }

        /// <summary>
        /// Character object 
        /// </summary>
        [JsonIgnore]
        public Character? Character { get; set; }
        /// <summary>
        /// ID of the spell
        /// </summary>
        [JsonIgnore]
        public int SpellId { get; set; }
        /// <summary>
        /// Spell object
        /// </summary>
        public Spell? Spell { get; set; }
    }

    /// <summary>
    /// Associative class for stats used by characters
    /// </summary>
    public class StatsInCharacter
    {
        /// <summary>
        /// ID of associative class
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// Character ID
        /// </summary>
        [JsonIgnore]
        public int CharacterId { get; set; }

        /// <summary>
        /// Character object
        /// </summary>
        [JsonIgnore]
        public Character? Character { get; set; }
        /// <summary>
        /// ID of stat
        /// </summary>
        [JsonIgnore]

        public int StatId { get; set; }
        /// <summary>
        /// If the character has proficiency in the stat (bonus score)
        /// </summary>
        public bool Proficiency { get; set; }
        /// <summary>
        /// The numerical score of the skill used by the character
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The stat itself
        /// </summary>

        public Stat? Stat { get; set; }
    }
    
    /// <summary>
    /// Associative class for skills used by characters
    /// </summary>
    public class SkillsInCharacter
    {
        /// <summary>
        /// Unique identifier for skills used by characters
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// Character's ID
        /// </summary>

        [JsonIgnore]
        public int CharacterId { get; set; }

        /// <summary>
        /// Character object
        /// </summary>
        [JsonIgnore]
        public Character? Character { get; set; }

        /// <summary>
        /// Skill's ID
        /// </summary>
        [JsonIgnore]
        public int SkillId { get; set; }

        /// <summary>
        /// The character's score for the skill
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// If the character is good at the skill
        /// </summary>
        public bool Proficiency { get; set; }

        /// <summary>
        /// Skill object
        /// </summary>
        public Skill? Skill { get; set; }

        /// <summary>
        /// Constructor for skills used by characters; used to set score
        /// </summary>
        public SkillsInCharacter()
        {

            //set skill score based on proficiency and skill's base stat score 
            var characterStat = Character.Stats.FirstOrDefault(s => s.Id == Skill.BaseStat.Id).Score;
            this.Score = characterStat += Proficiency ? 2 : 0;

        }
    }
}
