using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    /// <summary>
    /// Interface for stat repo
    /// </summary>
    public interface IStatRepository
    {
        Task<ICollection<Stat>> ReadAllAsync();
        Task<Stat?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Stat updatedStat);
        Task DeleteAsync(int id);
        Task<Stat> CreateAsync(Stat stat);  
    }
    //USER-FACING:
    // READ, READ-ALL

    //BACKEND:
    // CREATE, UPDATE, DELETE
    
    /// <summary>
    /// Implementation of <see cref="IStatRepository"/>
    /// </summary>
    /// <param name="db">the db to be used</param>
    public class StatRepository(ApplicationDbContext db) : IStatRepository
    {
        private readonly ApplicationDbContext _db = db;

        /// <summary>
        /// Reads all stats
        /// </summary>
        /// <returns>a collection of all stats</returns>
        public async Task<ICollection<Stat>> ReadAllAsync()
        {
            return await _db.Stats.ToListAsync();
        }

        /// <summary>
        /// Reads a single stat
        /// </summary>
        /// <param name="id">the id of the stat to be read</param>
        /// <returns>a stat, or null if it cant be found</returns>
        public async Task<Stat?> ReadAsync(int id)
        {
           return await _db.Stats.FindAsync(id);
        }
        /// <summary>
        /// Updates a stat
        /// </summary>
        /// <param name="oldId">the id of the stat to update</param>
        /// <param name="updatedStat">the updated stat</param>
        /// <returns></returns>
        public async Task UpdateAsync(int oldId, Stat updatedStat)
        {
            Stat? statToUpdate = await ReadAsync(oldId);
            if (statToUpdate != null)
            {
                statToUpdate.Id = updatedStat.Id;
                statToUpdate.Name = updatedStat.Name;
                await _db.SaveChangesAsync();
            }

        }
        /// <summary>
        /// creates a new stat
        /// </summary>
        /// <param name="newStat">the new stat to be added</param>
        /// <returns>a copy of the new stat</returns>
        public async Task<Stat> CreateAsync(Stat newStat)
        {
            await _db.Stats.AddAsync(newStat);
            await _db.SaveChangesAsync();
            return newStat;
        }
       
       /// <summary>
       /// Deletes a stat from the DB
       /// </summary>
       /// <param name="id">the id of the stat to be deleted</param>
       /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            Stat? statToDelete = await ReadAsync(id);
            if (statToDelete != null)
            {
                _db.Stats.Remove(statToDelete);
                await _db.SaveChangesAsync();
            }
        }

    }
}
