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
    public class CharacterRepository(ApplicationDbContext db) : ICharacterRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<Character> CreateAsync(Character character)
        {
            await _db.Characters.AddAsync(character);
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
