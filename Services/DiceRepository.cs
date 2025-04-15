using DnDWebApp_CC.Models.Entities;

namespace DnDWebApp_CC.Services
{
    public interface IDiceRepository
    {
        Task<ICollection<Dice>> ReadAllAsync();
        Task<Dice?> ReadAsync(int id);
    }

    public class DiceRepository(ApplicationDbContext db) : IDiceRepository
    {
        private readonly ApplicationDbContext _db = db;

        public Task<ICollection<Dice>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Dice?> ReadAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
    
}
