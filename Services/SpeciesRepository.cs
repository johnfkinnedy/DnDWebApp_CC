using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace DnDWebApp_CC.Services
{
    public interface ISpeciesRepository
    {
        Task<ICollection<Species>> ReadAllAsync();
        Task<Species?> ReadAsync(int id);
    }

    //USER-FACING:
    // READ, READ-ALL

    //BACKEND:
    // CREATE, UPDATE, DELETE
    public class SpeciesRepository(ApplicationDbContext db) : ISpeciesRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<Species> CreateAsync(Species speciesToAdd)
        {
            await _db.Species.AddAsync(speciesToAdd);
            await _db.SaveChangesAsync();
            return speciesToAdd;
        }
        public async Task<ICollection<Species>> ReadAllAsync()
        {
            return await _db.Species
                .Include(s => s.Skills)
                    .ThenInclude(s => s.Skill)
                .ToListAsync();
        }

        public async Task<Species?> ReadAsync(int id)
        {
            return await _db.Species
                .Include(s => s.Skills)
                    .ThenInclude(s => s.Skill)
                .FirstOrDefaultAsync(sp => sp.Id == id);
        }

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
