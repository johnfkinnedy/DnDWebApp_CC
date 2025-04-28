using DnDWebApp_CC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    public interface ICharacterClassRepository
    {
        Task<ICollection<CharacterClass>> ReadAllAsync();
        Task<CharacterClass> CreateAsync(CharacterClass newClass);
        Task<CharacterClass?> ReadAsync(int id);
        Task UpdateAsync(int oldId, CharacterClass characterClass);
        Task DeleteAsync(int id);
    }

    //USER-FACING:
    // READ, READ-ALL

    //BACKEND:
    // CREATE, UPDATE, DELETE
    public class CharacterClassRepository(ApplicationDbContext db) : ICharacterClassRepository
    {
        private readonly ApplicationDbContext _db = db;


        public async Task<CharacterClass> CreateAsync(CharacterClass newClass)
        {
            await _db.CharacterClasses.AddAsync(newClass);
            await _db.SaveChangesAsync();
            return newClass;

        }

        public async Task DeleteAsync(int id)
        {
            CharacterClass? classToDelete = await ReadAsync(id);
            if (classToDelete != null)
            {
                _db.CharacterClasses.Remove(classToDelete);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ICollection<CharacterClass>> ReadAllAsync()
        {
            return await _db.CharacterClasses
                .Include(c => c.Spells)
                    .ThenInclude(s => s.Spell)
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                .Include(c => c.HitDice)
                .ToListAsync();
          
        }

        public async Task<CharacterClass?> ReadAsync(int id)
        {
            return await _db.CharacterClasses.Include(c => c.Skills)
                .Include(c => c.Spells)
                    .ThenInclude(s => s.Spell)
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                .Include(c => c.HitDice)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(int oldId, CharacterClass characterClass)
        {
            CharacterClass? classToUpdate = await ReadAsync(oldId);
            if (classToUpdate != null)
            {
                //updating each value in character class
                classToUpdate.Id = characterClass.Id;
                classToUpdate.Name = characterClass.Name;
                classToUpdate.Features = characterClass.Features;
                classToUpdate.Proficiencies = characterClass.Proficiencies;
                classToUpdate.Skills = characterClass.Skills;
                classToUpdate.HitDice = characterClass.HitDice;
                classToUpdate.Spellcaster = characterClass.Spellcaster;
                classToUpdate.Spells = characterClass.Spells;
                classToUpdate.Description = characterClass.Description;

                await _db.SaveChangesAsync();
            }
        }
    }
}
