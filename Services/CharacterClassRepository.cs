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
            return await _db.CharacterClasses.Include(c => c.Skills)
                .Include(c => c.Spells)
                    .ThenInclude(s => s.Spell)
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                .ToListAsync();
        }

        public async Task<CharacterClass?> ReadAsync(int id)
        {
            return await _db.CharacterClasses.Include(c => c.Skills)
                .Include(c => c.Spells)
                    .ThenInclude(s => s.Spell)
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(int oldId, CharacterClass characterClass)
        {
            CharacterClass? classToUpdate = await ReadAsync(oldId);
            if (classToUpdate != null)
            {
                classToUpdate = characterClass;
                await _db.SaveChangesAsync();
            }
        }
    }
}
