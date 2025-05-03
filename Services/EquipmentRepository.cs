using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    public interface IEquipmentRepository
    {
        Task<ICollection<Equipment>> ReadAllAsync();
        Task<Equipment> CreateAsync(Equipment equipment);
        Task<Equipment?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Equipment equipment);
        Task DeleteAsync(int id);
        Task<bool> AssignCharacterAsync(int equipmentId, int characterId);
    }

    //USER-FACING:
    //CREATE, READ, READ-ALL, UPDATE, DELETE
    public class EquipmentRepository(ApplicationDbContext db) : IEquipmentRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<Equipment> CreateAsync(Equipment equipment)
        {
            await _db.Equipment.AddAsync(equipment);
            await _db.SaveChangesAsync();
            return equipment;
        }

        public async Task DeleteAsync(int id)
        {
            Equipment? eqToDelete = await ReadAsync(id);
            if (eqToDelete != null)
            {
                _db.Equipment.Remove(eqToDelete);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Equipment>> ReadAllAsync()
        {
            return await _db.Equipment.ToListAsync();
        }

        public async Task<Equipment?> ReadAsync(int id)
        {
            return await _db.Equipment.FindAsync(id);
        }

        public async Task UpdateAsync(int oldId, Equipment equipment)
        {
            Equipment? eqToUpdate = await ReadAsync(oldId);
            if (eqToUpdate != null)
            {
                eqToUpdate.Id = equipment.Id;
                eqToUpdate.Name = equipment.Name;
                eqToUpdate.Description = equipment.Description;
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> AssignCharacterAsync(int equipmentId, int characterId)
        {
            Equipment? equipment = await ReadAsync(equipmentId);
            if (equipment != null)
            {
                Character? character = await _db.Characters.FirstOrDefaultAsync(c => c.Id == characterId);

                EquipmentInCharacter? eqInCharacter = character.Equipment.FirstOrDefault(e => e.Id == equipmentId);
                if (eqInCharacter == null)
                {
                    EquipmentInCharacter newCharEquipment = new EquipmentInCharacter
                    {
                        Character = character,
                        CharacterId = character.Id,
                        EquipmentId = equipment.Id,
                        Equipment = equipment
                    };
                    _db.EquipmentInCharacters.Add(newCharEquipment);
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }

    }
}
