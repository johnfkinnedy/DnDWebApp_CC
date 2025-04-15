using DnDWebApp_CC.Models.Entities;

namespace DnDWebApp_CC.Services
{
    public interface ISkillRepository
    {
        Task<ICollection<Skill>> ReadAllAsync();
        Task<Skill?> ReadAsync(int id);
    }

    public class SkillRepository(ApplicationDbContext db) : ISkillRepository
    {
        private readonly ApplicationDbContext _db = db;

        public Task<ICollection<Skill>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Skill?> ReadAsync(int id)
        {
            return await _db.Skills.FindAsync(id);
        }
    }
}
