using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace DnDWebApp_CC.Services
{
    /// <summary>
    /// Interface for a species repository
    /// </summary>
    public interface ISpeciesRepository
    {
        Task<ICollection<Species>> ReadAllAsync();
        Task<Species?> ReadAsync(int id);
    }

    //USER-FACING:
    // READ, READ-ALL

    //BACKEND:
    // CREATE, UPDATE, DELETE

    /// <summary>
    /// Implementation of <see cref="ISpeciesRepository"/>
    /// </summary>
    /// <param name="db">database context to be used</param>
    public class SpeciesRepository(ApplicationDbContext db) : ISpeciesRepository
    {
        private readonly ApplicationDbContext _db = db;

        /// <summary>
        /// Creates a new species; not available to users
        /// </summary>
        /// <param name="speciesToAdd">the species to add</param>
        /// <returns>a copy of the added species</returns>
        public async Task<Species> CreateAsync(Species speciesToAdd)
        {
            await _db.Species.AddAsync(speciesToAdd);
            await _db.SaveChangesAsync();
            return speciesToAdd;
        }
        /// <summary>
        /// Reads all species
        /// </summary>
        /// <returns>a collection of all species in the database</returns>
        public async Task<ICollection<Species>> ReadAllAsync()
        {
            return await _db.Species
                .Include(s => s.Skills)
                    .ThenInclude(s => s.Skill)
                .ToListAsync();
        }

        /// <summary>
        /// reads a single species 
        /// </summary>
        /// <param name="id">the id of the species to read</param>
        /// <returns>a species, or null if it isnt found</returns>
        public async Task<Species?> ReadAsync(int id)
        {
            return await _db.Species
                .Include(s => s.Skills)
                    .ThenInclude(s => s.Skill)
                .FirstOrDefaultAsync(sp => sp.Id == id);
        }

        /// <summary>
        /// Updates a species
        /// </summary>
        /// <param name="oldId">the id of the species to update</param>
        /// <param name="species">the updated species</param>
        /// <returns></returns>
        public async Task UpdateSpecies(int oldId, Species species)
        {
            Species? speciesToUpdate = await ReadAsync(oldId);
            if(speciesToUpdate != null)
            {
                speciesToUpdate.Id = species.Id;
                speciesToUpdate.Name = species.Name;
                speciesToUpdate.Description = species.Description;
                speciesToUpdate.Languages = species.Languages;
                speciesToUpdate.Skills = species.Skills;
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes a species from the database
        /// </summary>
        /// <param name="id">the id of the species to be deleted</param>
        /// <returns></returns>
        public async Task DeleteSpecies(int id)
        {
            Species? speciesToDelete = await ReadAsync(id);
            if (speciesToDelete != null)
            {
                _db.Species.Remove(speciesToDelete);
                await _db.SaveChangesAsync();
            }

        }
    }
}
