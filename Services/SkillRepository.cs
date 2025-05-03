using DnDWebApp_CC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    /// <summary>
    /// Interface for skill repository
    /// </summary>
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

    /// <summary>
    /// Implementation of <see cref="ISkillRepository"/>
    /// </summary>
    /// <param name="db">the db to be used</param>
    public class SkillRepository(ApplicationDbContext db) : ISkillRepository
    {
        private readonly ApplicationDbContext _db = db;
        
        /// <summary>
        /// Creates a new skill
        /// </summary>
        /// <param name="newSkill">the skill to be creaed</param>
        /// <returns>a copy of the skill</returns>
        public async Task<Skill> CreateAsync(Skill newSkill)
        {
            await _db.Skills.AddAsync(newSkill);
            await _db.SaveChangesAsync();
            return newSkill;
        }

        /// <summary>
        /// Reads all skills
        /// </summary>
        /// <returns>a collection of all skills</returns>
        public async Task<ICollection<Skill>> ReadAllAsync()
        {
            return await _db.Skills.ToListAsync();
        }
        /// <summary>
        /// Reads a single skill
        /// </summary>
        /// <param name="id">The id of the skill to be read</param>
        /// <returns>a skill, or null if it can't be found</returns>

        public async Task<Skill?> ReadAsync(int id)
        {
            return await _db.Skills.FindAsync(id);
        }

        /// <summary>
        /// Updates a skill
        /// </summary>
        /// <param name="oldId">the id of the skill to be updated</param>
        /// <param name="skill">the updated skill</param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Deletes a skill
        /// </summary>
        /// <param name="id">the id of the skill to be deleted</param>
        /// <returns></returns>
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
