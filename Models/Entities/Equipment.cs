using System.ComponentModel.DataAnnotations;

namespace DnDWebApp_CC.Models.Entities
{
    /// <summary>
    /// Refers to something that can be equipped or used by a <see cref="Player"/>
    /// </summary>
    public class Equipment
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of the equipment item
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the equipment item; what it does
        /// </summary>
        public string Description { get; set; } = string.Empty; 
        
        public ICollection<EquipmentInCharacter> UsedByCharacters { get; set; } = new List<EquipmentInCharacter>();
    }
}
