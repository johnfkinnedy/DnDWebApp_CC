using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;

namespace DnDWebApp_CC.Services
{
    public interface ISpeciesRepository
    {
        Task<ICollection<Species>> ReadAllAsync();
        Task<Species?> ReadAsync(int id);
    }

    public class SpeciesRepository(ApplicationDbContext db) : ISpeciesRepository
    {
        private readonly ApplicationDbContext _db = db;

        public Task<ICollection<Species>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Species?> ReadAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
