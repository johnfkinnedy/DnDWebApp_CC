using DnDWebApp_CC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    /// <summary>
    /// Interface for dice repo
    /// </summary>
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

    /// <summary>
    /// Implementation of dice repo
    /// </summary>
    /// <param name="db">the database to be used</param>
    public class DiceRepository(ApplicationDbContext db) : IDiceRepository
    {
        private readonly ApplicationDbContext _db = db;

        /// <summary>
        /// Creates a new dice
        /// </summary>
        /// <param name="dice">dice to be created</param>
        /// <returns>a copy of the created dice </returns>
        public async Task<Dice> CreateAsync(Dice dice)
        {
            await _db.Dice.AddAsync(dice);
            await _db.SaveChangesAsync();
            return dice;
        }

        /// <summary>
        /// Deletes a dice from the database
        /// </summary>
        /// <param name="id">the id of the dice to be deleted</param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            Dice? diceToDelete = await ReadAsync(id);
            if (diceToDelete != null)
            {
                _db.Dice.Remove(diceToDelete);
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Reads all dice
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Dice>> ReadAllAsync()
        {
            return await _db.Dice.ToListAsync();
        }

        /// <summary>
        /// Reads a single dice
        /// </summary>
        /// <param name="id">the id of the dice</param>
        /// <returns>a dice, or null if it doesnt exist</returns>
        public async Task<Dice?> ReadAsync(int id)
        {
            return await _db.Dice.FindAsync(id);
        }

        /// <summary>
        /// Updates a dice
        /// </summary>
        /// <param name="oldId">the old id</param>
        /// <param name="dice">the dice to be updated</param>
        /// <returns></returns>
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
