using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;

namespace DnDWebApp_CC.Services
{
    public interface IEquipmentRepository
    {
        Task<ICollection<Equipment>> ReadAllAsync();
        Task<Equipment> CreateAsync(Equipment equipment);
        Task<Equipment?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Equipment equipment);
        Task DeleteAsync(int id);
    }

    public class EquipmentRepository(ApplicationDbContext db) : IEquipmentRepository
    {
        private readonly ApplicationDbContext _db = db;

        public Task<Equipment> CreateAsync(Equipment equipment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Equipment>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Equipment?> ReadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int oldId, Equipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}
