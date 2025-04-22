using DnDWebApp_CC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    public interface IDiceRepository
    {
        Task<ICollection<Dice>> ReadAllAsync();
        Task<Dice?> ReadAsync(int id);
        Task<Dice> CreateAsync(Dice dice);
        Task UpdateAsync(int oldId, Dice dice);
        Task DeleteAsync(int id);
    }

    //USER-FACING:
    // READ, READ-ALL

    //BACKEND:
    // CREATE, UPDATE, DELETE
    public class DiceRepository(ApplicationDbContext db) : IDiceRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<Dice> CreateAsync(Dice dice)
        {
            await _db.Dice.AddAsync(dice);
            await _db.SaveChangesAsync();
            return dice;
        }

        public async Task DeleteAsync(int id)
        {
            Dice? diceToDelete = await ReadAsync(id);
            if (diceToDelete != null)
            {
                _db.Dice.Remove(diceToDelete);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Dice>> ReadAllAsync()
        {
            return await _db.Dice.ToListAsync();
        }

        public async Task<Dice?> ReadAsync(int id)
        {
            return await _db.Dice.FindAsync(id);
        }

        public async Task UpdateAsync(int oldId, Dice dice)
        {
            Dice? diceToUpdate = await ReadAsync(oldId);
            if (diceToUpdate != null)
            {
                diceToUpdate.Id = dice.Id;
                diceToUpdate.Size = dice.Size;
                await _db.SaveChangesAsync();
            }
        }
    }
    
}
