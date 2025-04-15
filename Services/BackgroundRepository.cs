using DnDWebApp_CC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    public interface IBackgroundRepository
    {
        Task<ICollection<Background>> ReadAllAsync();
        Task<Background> CreateAsync(Background newBg);
        Task<Background?> ReadAsync(string name);
        Task UpdateAsync(string oldName, Background bg);
        Task DeleteAsync(string name);
    }
    public class BackgroundRepository(ApplicationDbContext db) : IBackgroundRepository
    {
        private readonly ApplicationDbContext _db = db;
        public async Task<ICollection<Background>> ReadAllAsync()
        {
            return await _db.Backgrounds.Include(b => b.Skills).ThenInclude(s => s.Skill).ThenInclude(s => s.BaseStat).ToListAsync();
        }

        public async Task<Background> CreateAsync(Background newBg)
        {
            await _db.Backgrounds.AddAsync(newBg);
            await _db.SaveChangesAsync();
            return newBg;
        }

        public async Task<Background?> ReadAsync(string name)
        {
            return await _db.Backgrounds.Include(b => b.Skills).ThenInclude(s => s.Skill).FirstOrDefaultAsync(b => b.Name == name);
        }

        public async Task UpdateAsync(string oldName, Background bg)
        {
            Background? bgToUpdate = await ReadAsync(oldName);
            if (bgToUpdate != null)
            {
                bgToUpdate = bg;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(string name)
        {
            Background? bgToDelete = await ReadAsync(name);
            if (bgToDelete != null)
            {
                _db.Backgrounds.Remove(bgToDelete);
                await _db.SaveChangesAsync();
            }
        }

    }
}
