using DnDWebApp_CC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    public interface ISkillRepository
    {
        Task<ICollection<Skill>> ReadAllAsync();
        Task<Skill?> ReadAsync(int id);
        Task<Skill> CreateAsync(Skill skill);
        Task UpdateAsync(int oldId, Skill skill);
        Task DeleteAsync(int id);
    }

    //USER-FACING:
    // READ, READ-ALL

    //BACKEND:
    // CREATE, UPDATE, DELETE
    public class SkillRepository(ApplicationDbContext db) : ISkillRepository
    {
        private readonly ApplicationDbContext _db = db;
        
        public async Task<Skill> CreateAsync(Skill newSkill)
        {
            await _db.Skills.AddAsync(newSkill);
            await _db.SaveChangesAsync();
            return newSkill;
        }
        public async Task<ICollection<Skill>> ReadAllAsync()
        {
            return await _db.Skills.ToListAsync();
        }

        public async Task<Skill?> ReadAsync(int id)
        {
            return await _db.Skills.FindAsync(id);
        }

        public async Task UpdateAsync(int oldId, Skill skill)
        {
            Skill? skillToUpdate = await ReadAsync(oldId);
            if (skillToUpdate != null)
            {
                skillToUpdate.Id = skill.Id;
                skillToUpdate.Name = skill.Name;
                skillToUpdate.Description = skill.Description;
                skillToUpdate.BaseStat = skill.BaseStat;
                await _db.SaveChangesAsync();
            }
        }
        
        public async Task DeleteAsync(int id)
        {
            Skill? skillToDelete = await ReadAsync(id);
            if (skillToDelete != null)
            {
                _db.Skills.Remove(skillToDelete);
                await _db.SaveChangesAsync();
            }
        }
    }
}
