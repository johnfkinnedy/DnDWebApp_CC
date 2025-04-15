using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    public interface IStatRepository
    {
        Task<ICollection<Stat>> ReadAllAsync();
        Task<Stat?> ReadAsync(int id);
    }

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
    }
}
