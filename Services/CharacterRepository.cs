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

    public class CharacterRepository(ApplicationDbContext db) : ICharacterRepository
    {
        private readonly ApplicationDbContext _db = db;

        public Task<Character> CreateAsync(Character character)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Character>> ReadAllAsync()
        {
            return await _db.Characters
                .Include(c => c.Skills)
                .Include(c => c.Stats)
                .Include(c => c.Spells)
                .Include(c => c.Class)
                .Include(c => c.Background)
                .Include(c => c.Species)
                .Include(c => c.SecondClass)
                .Include(c => c.Equipment)
                .ToListAsync();
        }

        public Task<Character?> ReadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int oldId, Character character)
        {
            throw new NotImplementedException();
        }
    }
}
