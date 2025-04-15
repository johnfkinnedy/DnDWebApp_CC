using DnDWebApp_CC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace DnDWebApp_CC.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // public DbSet<class> Name => Set<class

        public DbSet<Stat> Stats => Set<Stat>();
        public DbSet<Dice> Dice => Set<Dice>();
        public DbSet<Skill> Skills => Set<Skill>();
        public DbSet<Equipment> Equipment => Set<Equipment>();
        public DbSet<Spell> Spells => Set<Spell>();
        public DbSet<DiceInSpells> DiceInSpells => Set<DiceInSpells>();
        public DbSet<CharacterClass> CharacterClasses => Set<CharacterClass>();
        public DbSet<ClassSkills> ClassSkills => Set<ClassSkills>();
        public DbSet<ClassSpell> ClassSpells => Set<ClassSpell>();
        public DbSet<Species> Species => Set<Species>();
        public DbSet<SkillsInSpecies> SkillsInSpecies => Set<SkillsInSpecies>();
        public DbSet<Background> Backgrounds => Set<Background>();
        public DbSet<SkillInBackground> SkillsInBackgrounds => Set<SkillInBackground>();
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<EquipmentInCharacter> EquipmentInCharacters => Set<EquipmentInCharacter>();
        public DbSet<StatsInCharacter> StatsInCharacters => Set<StatsInCharacter>();
        public DbSet<SkillsInCharacter> SkillsInCharacters => Set<SkillsInCharacter>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

