using DnDWebApp_CC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    /// <summary>
    /// Interface for a Character Class Repository
    /// </summary>
    public interface ICharacterClassRepository
    {
        Task<ICollection<CharacterClass>> ReadAllAsync();
        Task<CharacterClass> CreateAsync(CharacterClass newClass);
        Task<CharacterClass?> ReadAsync(int id);
        Task UpdateAsync(int oldId, CharacterClass characterClass);
        Task DeleteAsync(int id);
    }

    //USER-FACING:
    // READ, READ-ALL

    //BACKEND:
    // CREATE, UPDATE, DELETE

    /// <summary>
    /// Implementation of <see cref="ICharacterClassRepository"/>
    /// </summary>
    /// <param name="db">The database context</param>
    public class CharacterClassRepository(ApplicationDbContext db) : ICharacterClassRepository
    {
        private readonly ApplicationDbContext _db = db;


        /// <summary>
        /// Creates a new character class
        /// </summary>
        /// <param name="newClass">the character class to be added </param>
        /// <returns>the class that was just added</returns>
        public async Task<CharacterClass> CreateAsync(CharacterClass newClass)
        {
            await _db.CharacterClasses.AddAsync(newClass);
            await _db.SaveChangesAsync();
            return newClass;

        }

        /// <summary>
        /// Deletes a class from the context
        /// </summary>
        /// <param name="id">the id of the class to be deleted</param>
        /// <returns>nothing</returns>
        public async Task DeleteAsync(int id)
        {
            CharacterClass? classToDelete = await ReadAsync(id);
            if (classToDelete != null)
            {
                _db.CharacterClasses.Remove(classToDelete);
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Reads all classes from the database
        /// </summary>
        /// <returns>a list of all classes</returns>
        public async Task<ICollection<CharacterClass>> ReadAllAsync()
        {
            return await _db.CharacterClasses
                .Include(c => c.Spells)
                    .ThenInclude(s => s.Spell)
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                    .ThenInclude(s => s.BaseStat)
                .Include(c => c.HitDice)
                .ToListAsync();
          
        }

        /// <summary>
        /// Reads a single class from the database
        /// </summary>
        /// <param name="id">the id of the class to be read</param>
        /// <returns>the class, or null if it isnt found</returns>
        public async Task<CharacterClass?> ReadAsync(int id)
        {
            return await _db.CharacterClasses.Include(c => c.Skills)
                .Include(c => c.Spells)
                    .ThenInclude(s => s.Spell)
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                .Include(c => c.HitDice)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Updates a class
        /// </summary>
        /// <param name="oldId">the id of the class to be updated</param>
        /// <param name="characterClass">the updated class</param>
        /// <returns>nothing</returns>
        public async Task UpdateAsync(int oldId, CharacterClass characterClass)
        {
            CharacterClass? classToUpdate = await ReadAsync(oldId);
            if (classToUpdate != null)
            {
                //updating each value in character class
                classToUpdate.Id = characterClass.Id;
                classToUpdate.Name = characterClass.Name;
                classToUpdate.Features = characterClass.Features;
                classToUpdate.Proficiencies = characterClass.Proficiencies;
                classToUpdate.Skills = characterClass.Skills;
                classToUpdate.HitDice = characterClass.HitDice;
                classToUpdate.Spellcaster = characterClass.Spellcaster;
                classToUpdate.Spells = characterClass.Spells;
                classToUpdate.Description = characterClass.Description;

                await _db.SaveChangesAsync();
            }
        }
    }
}
