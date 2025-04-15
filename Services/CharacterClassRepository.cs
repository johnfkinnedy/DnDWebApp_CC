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

        public Task<CharacterClass> CreateAsync(CharacterClass newClass)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<CharacterClass>> ReadAllAsync()
        {
            return await _db.CharacterClasses.Include(c => c.Skills)
                .Include(c => c.Spells)
                .ToListAsync();
        }

        public Task<CharacterClass?> ReadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int oldId, CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }
    }
}
