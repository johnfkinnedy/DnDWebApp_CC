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

    //USER-FACING:
    //CREATE, READ, READ-ALL, UPDATE, DELETE
    public class SpellRepository(ApplicationDbContext db) : ISpellRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<Spell> CreateAsync(Spell newSpell)
        {
            await _db.AddAsync(newSpell);
            await _db.SaveChangesAsync();
            return newSpell;
        }

        

        public async Task<ICollection<Spell>> ReadAllAsync()
        {
            return await _db.Spells
                .Include(s => s.DiceDenomination)
                    .ToListAsync();
        }

        public async Task<Spell?> ReadAsync(int id)
        {
            return await _db.Spells
                            .Include(s => s.DiceDenomination)
                                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(int oldId, Spell spell)
        {
            Spell? spellToUpdate = await ReadAsync(oldId);
            if (spellToUpdate != null)
            {
                spellToUpdate.Id = spell.Id;
                spellToUpdate.Name = spell.Name;
                spellToUpdate.Description = spell.Description;
                spellToUpdate.DiceDenomination = spell.DiceDenomination;
                spellToUpdate.DiceToRoll = spell.DiceToRoll;
                spellToUpdate.SlotLevel = spell.SlotLevel;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            Spell? spellToDelete = await ReadAsync(id);
            if (spellToDelete != null)
            {
                _db.Spells.Remove(spellToDelete);
                await _db.SaveChangesAsync();
            }
        }
    }

}
