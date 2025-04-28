using DnDWebApp_CC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    public interface ICharacterRepository
    {
        Task<ICollection<Character>> ReadAllAsync();
        Task<Character> CreateAsync(Character character);
        Task<Character?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Character character);
        Task DeleteAsync(int id);
    }

    //USER-FACING:
    //CREATE, READ, READ-ALL, UPDATE, DELETE
    public class CharacterRepository(ApplicationDbContext db, IBackgroundRepository bgRepo, ICharacterClassRepository classRepo, ISpeciesRepository speciesRepo) : ICharacterRepository
    {
        private readonly ApplicationDbContext _db = db;

        private readonly IBackgroundRepository _bgRepo = bgRepo;
        private readonly ISpeciesRepository _speciesRepo = speciesRepo;
        private readonly ICharacterClassRepository _classRepo = classRepo;

        public async Task<Character> CreateAsync(Character character)
        {
            await _db.Characters.AddAsync(character);
            //assigning class, background, and ID after character is added without them
            var charClass = await _classRepo.ReadAsync(character.ClassId);
            var background = await _bgRepo.ReadAsync(character.BackgroundId);
            var species = await _speciesRepo.ReadAsync(character.SpeciesId);
            var stats = await _db.Stats.ToListAsync();
            var skills = await _db.Skills.Include(s => s.BaseStat).ToListAsync();

            //populating spells if the class has them
            if (charClass.Spellcaster)
            {
                foreach(var spell in charClass.Spells)
                {
                    character.Spells.Add(new SpellsInCharacter
                    {
                        Id = spell.Id,
                    });
                }
            }
            
            
            //populating features
            foreach(string feature in background.Features) { character.Features.Add(feature); }
            foreach(string feature in charClass.Features) { character.Features.Add(feature); }

            //populating languages
            foreach(string language in background.Languages) { character.Languages.Add(language); }
            foreach(string language in species.Languages) { character.Languages.Add(language); }

            //adding stats, and giving them base values to be changed later (in edit screen)
            foreach(Stat stat in stats)
            {
                character.Stats.Add(new StatsInCharacter { StatId = stat.Id, Score = 10, Proficiency = false});
            }


            //giving character every skill. scores will be set on character update, when scores are ACTUALLY set
            foreach (Skill skill in skills)
            {
                character.Skills.Add(new SkillsInCharacter { Id = skill.Id, Proficiency = false });
            }

            //but giving them proficiency in the skills perscribed by their class, background, and species
            foreach (var skill in charClass.Skills)
            {
                var profSkill = character.Skills.FirstOrDefault(s => s.Id == skill.Id);
                profSkill.Proficiency = true;
            }
            foreach (var skill in background.Skills)
            {
                var profSkill = character.Skills.FirstOrDefault(s => s.Id == skill.Id);
                profSkill.Proficiency = true;
            }
            foreach (var skill in species.Skills)
            {
                var profSkill = character.Skills.FirstOrDefault(s => s.Id == skill.Id);
                profSkill.Proficiency = true;
            }
            await _db.SaveChangesAsync();
            return character;
        }

        public async Task DeleteAsync(int id)
        {
            Character? charToDelete = await ReadAsync(id);
            if(charToDelete != null)
            {
                _db.Characters.Remove(charToDelete);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Character>> ReadAllAsync()
        {
            return await _db.Characters
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                .Include(c => c.Skills)
                .Include(c => c.Skills)
                    //.ThenInclude(s => s.Proficiency)
                .Include(c => c.Stats)
                    .ThenInclude(s => s.Stat)
                .Include(c => c.Stats)
                   // .ThenInclude(s => s.Score)
                .Include(c => c.Stats)
                   // .ThenInclude(s => s.Proficiency)
                .Include(c => c.Spells)
                .Include(c => c.Class)
                .Include(c => c.Background)
                .Include(c => c.Species)
                .Include(c => c.SecondClass)
                .Include(c => c.Equipment)
                    .ThenInclude(e => e.Equipment)
                .ToListAsync();
        }

        public async Task<Character?> ReadAsync(int id)
        {
            return await _db.Characters
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Score)
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Proficiency)
                .Include(c => c.Stats)
                    .ThenInclude(s => s.Stat)
                .Include(c => c.Stats)
                    .ThenInclude(s => s.Score)
                .Include(c => c.Stats)
                    .ThenInclude(s => s.Proficiency)
                .Include(c => c.Spells)
                .Include(c => c.Class)
                .Include(c => c.Background)
                .Include(c => c.Species)
                .Include(c => c.SecondClass)
                .Include(c => c.Equipment)
                    .ThenInclude(e => e.Equipment)
                .FirstOrDefaultAsync(c => c.Id == id);

            
        }

        public async Task UpdateAsync(int oldId, Character character)
        {
            Character? charToUpdate = await ReadAsync(oldId);
            if(charToUpdate != null)
            {
                //updating each value in character
                charToUpdate.Id = character.Id;
                charToUpdate.Name = character.Name;
                charToUpdate.Age = character.Age;
                charToUpdate.Alignment = character.Alignment;
                charToUpdate.Class = character.Class;
                charToUpdate.SecondClass = character.SecondClass;
                charToUpdate.Species = character.Species;
                charToUpdate.Background = character.Background;
                charToUpdate.Equipment = character.Equipment;
                charToUpdate.Spells = character.Spells;
                charToUpdate.Stats = character.Stats;
                charToUpdate.ProficiencyBonus = character.ProficiencyBonus;
                charToUpdate.Level = character.Level;
                charToUpdate.Skills = character.Skills;
                charToUpdate.Features = character.Features;
                charToUpdate.Languages = character.Languages;

                await _db.SaveChangesAsync();
            }
            
        }
    }
}
