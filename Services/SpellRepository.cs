using DnDWebApp_CC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    public interface ISpellRepository
    {
        Task<ICollection<Spell>> ReadAllAsync();
        Task<Spell> CreateAsync(Spell newSpell);
        Task<Spell?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Spell spell);
        Task DeleteAsync(int id);
    }

    public class SpellRepository(ApplicationDbContext db) : ISpellRepository
    {
        private readonly ApplicationDbContext _db = db;

        public Task<Spell> CreateAsync(Spell newSpell)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Spell>> ReadAllAsync()
        {
            return await _db.Spells
                .Include(s => s.DiceDenomination)
                    .ToListAsync();
        }

        public Task<Spell?> ReadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int oldId, Spell spell)
        {
            throw new NotImplementedException();
        }
    }
}
