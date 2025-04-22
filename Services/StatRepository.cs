using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
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

    public class StatRepository(ApplicationDbContext db) : IStatRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<ICollection<Stat>> ReadAllAsync()
        {
            return await _db.Stats.ToListAsync();
        }

        public async Task<Stat?> ReadAsync(int id)
        {
           return await _db.Stats.FindAsync(id);
        }
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
        public async Task<Stat> CreateAsync(Stat newStat)
        {
            await _db.Stats.AddAsync(newStat);
            await _db.SaveChangesAsync();
            return newStat;
        }
       
        
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
