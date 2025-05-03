using DnDWebApp_CC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    /// <summary>
    /// Interface for a background repository
    /// </summary>
    public interface IBackgroundRepository
    {
        Task<ICollection<Background>> ReadAllAsync();
        Task<Background> CreateAsync(Background newBg);
        Task<Background?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Background bg);
        Task DeleteAsync(int id);
    }
    //USER-FACING:
    // READ, READ-ALL

    //BACKEND:
    // CREATE, UPDATE, DELETE

    /// <summary>
    /// Implementation of <see cref="IBackgroundRepository"/>
    /// </summary>
    /// <param name="db">The database context to be used</param>
    public class BackgroundRepository(ApplicationDbContext db) : IBackgroundRepository
    {
        private readonly ApplicationDbContext _db = db;
        /// <summary>
        /// Reads all backgrounds
        /// </summary>
        /// <returns>a list of all backgrounds</returns>
        public async Task<ICollection<Background>> ReadAllAsync()
        {
            return await _db.Backgrounds.Include(b => b.Skills).ThenInclude(s => s.Skill).ThenInclude(s => s.BaseStat).ToListAsync();
        }
        /// <summary>
        /// Creates a new background
        /// </summary>
        /// <param name="newBg">the background to be added</param>
        /// <returns>the background that was just created</returns>
        public async Task<Background> CreateAsync(Background newBg)
        {
            await _db.Backgrounds.AddAsync(newBg);
            await _db.SaveChangesAsync();
            return newBg;
        }

        /// <summary>
        /// Reads a single background
        /// </summary>
        /// <param name="id">the id of the background to be read</param>
        /// <returns>the background with a matching id, or null if it wasn't found</returns>
        public async Task<Background?> ReadAsync(int id)
        {
            return await _db.Backgrounds
                .Include(b => b.Skills)
                .ThenInclude(s => s.Skill)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        /// <summary>
        /// Updates a background
        /// </summary>
        /// <param name="oldId">The ID of the background to be updated</param>
        /// <param name="bg">a new copy of the updated background</param>
        /// <returns>nothing</returns>
        public async Task UpdateAsync(int oldId, Background bg)
        {
            Background? bgToUpdate = await ReadAsync(oldId);
            if (bgToUpdate != null)
            {
                //updating each value in background
                bgToUpdate.Id = bg.Id;
                bgToUpdate.Name = bg.Name;
                bgToUpdate.Description = bg.Description;
                bgToUpdate.Features = bg.Features;
                bgToUpdate.Languages = bg.Languages;
                bgToUpdate.Skills = bg.Skills;

                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes a background from the database
        /// </summary>
        /// <param name="id">the id of the background to be deleted</param>
        /// <returns>nothing</returns>
        public async Task DeleteAsync(int id)
        {
            Background? bgToDelete = await ReadAsync(id);
            if (bgToDelete != null)
            {
                _db.Backgrounds.Remove(bgToDelete);
                await _db.SaveChangesAsync();
            }
        }

    }
}
