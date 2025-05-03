using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Services
{
    /// <summary>
    /// Interface for equipment repo
    /// </summary>
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
    /// <summary>
    /// Implementation of <see cref="IEquipmentRepository"/>
    /// </summary>
    /// <param name="db">the dbcontext to be used </param>
    public class EquipmentRepository(ApplicationDbContext db) : IEquipmentRepository
    {
        private readonly ApplicationDbContext _db = db;

        /// <summary>
        /// Creates a new piece of equipment
        /// </summary>
        /// <param name="equipment">the equipment to be added</param>
        /// <returns>a copy of the equipment</returns>
        public async Task<Equipment> CreateAsync(Equipment equipment)
        {
            await _db.Equipment.AddAsync(equipment);
            await _db.SaveChangesAsync();
            return equipment;
        }

        /// <summary>
        /// Deletes an equipment from the DB
        /// </summary>
        /// <param name="id">ID Of the equipment to be deleted</param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            Equipment? eqToDelete = await ReadAsync(id);
            if (eqToDelete != null)
            {
                _db.Equipment.Remove(eqToDelete);
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Reads all equipment from DB
        /// </summary>
        /// <returns>a collection of all equipment</returns>
        public async Task<ICollection<Equipment>> ReadAllAsync()
        {
            return await _db.Equipment.ToListAsync();
        }

        /// <summary>
        /// Reads a single equipment item from DB
        /// </summary>
        /// <param name="id">the ID of the equipment</param>
        /// <returns>an equipment item, or null if it doesn't exist</returns>
        public async Task<Equipment?> ReadAsync(int id)
        {
            return await _db.Equipment.FindAsync(id);
        }

        /// <summary>
        /// Updates an equipment item
        /// </summary>
        /// <param name="oldId">the id of the equipment to be updated </param>
        /// <param name="equipment">the updated equipment item</param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Assigns an equipment item to a character
        /// </summary>
        /// <param name="equipmentId">the id of the equipment item to be assigned</param>
        /// <param name="characterId">the id of the character for the equipmnt to be assigned to </param>
        /// <returns></returns>
        public async Task<bool> AssignCharacterAsync(int equipmentId, int characterId)
        {
            Equipment? equipment = await ReadAsync(equipmentId);
            //reads an equipment, returns false if it doesnt exist
            if (equipment != null)
            {
                //reads a character
                Character? character = await _db.Characters.FirstOrDefaultAsync(c => c.Id == characterId);

                //checks if the equipment is already used by that character
                EquipmentInCharacter? eqInCharacter = character.Equipment.FirstOrDefault(e => e.Id == equipmentId);
                if (eqInCharacter == null) //if it is, return false
                {
                    //if not, assigns that equipment to that character
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
