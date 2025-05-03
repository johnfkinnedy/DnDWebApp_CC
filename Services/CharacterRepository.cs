using DnDWebApp_CC.Models.Entities;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DnDWebApp_CC.Services
{
    /// <summary>
    /// Interface for the character repository
    /// </summary>
    public interface ICharacterRepository
    {
        Task<ICollection<Character>> ReadAllAsync();
        Task<Character> CreateAsync(Character character);
        Task<Character?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Character character);
        Task DeleteAsync(int id);
        Task<byte[]> CreatePdf(int characterId);

    }

    //USER-FACING:
    //CREATE, READ, READ-ALL, UPDATE, DELETE
    /// <summary>
    /// Implementation of <see cref="ICharacterRepository"/>
    /// </summary>
    /// <param name="db">the database to be used</param>
    /// <param name="bgRepo">background repo</param>
    /// <param name="classRepo">class repo</param>
    /// <param name="speciesRepo">species repo</param>
    public class CharacterRepository(ApplicationDbContext db, IBackgroundRepository bgRepo, ICharacterClassRepository classRepo, ISpeciesRepository speciesRepo) : ICharacterRepository
    {
        private readonly ApplicationDbContext _db = db;

        private readonly IBackgroundRepository _bgRepo = bgRepo;
        private readonly ISpeciesRepository _speciesRepo = speciesRepo;
        private readonly ICharacterClassRepository _classRepo = classRepo;

        /// <summary>
        /// Creates a new character
        /// </summary>
        /// <param name="character">the character to be added</param>
        /// <returns>a copy of the created character</returns>
        public async Task<Character> CreateAsync(Character character)
        {
            await _db.Characters.AddAsync(character);
            //assigning class, background, and species after character is added without them
            var charClass = await _classRepo.ReadAsync(character.ClassId);
            var background = await _bgRepo.ReadAsync(character.BackgroundId);
            var species = await _speciesRepo.ReadAsync(character.SpeciesId);
            var stats = await _db.Stats.ToListAsync();
            var skills = await _db.Skills.Include(s => s.BaseStat).ToListAsync();

            //populating spells if the class has them
            if (charClass.Spellcaster)
            {
                foreach(var spell in charClass.Spells)
                {
                    character.Spells.Add(new SpellsInCharacter
                    {
                        Id = spell.Id,
                    });
                }
            }
            
            
            //populating features
            foreach(string feature in background.Features) { character.Features.Add(feature); }
            foreach(string feature in charClass.Features) { character.Features.Add(feature); }

            //populating languages
            foreach(string language in background.Languages) { character.Languages.Add(language); }
            foreach(string language in species.Languages) { character.Languages.Add(language); }

            //adding stats, and giving them base values to be changed later (in edit screen)
            foreach(Stat stat in stats)
            {
                character.Stats.Add(new StatsInCharacter { StatId = stat.Id, Score = 10, Proficiency = false});
            }


            //giving character every skill. scores will be set on character update, when scores are ACTUALLY set
            foreach (Skill skill in skills)
            {
                character.Skills.Add(new SkillsInCharacter { Id = skill.Id, Proficiency = false });
            }

            //but giving them proficiency in the skills perscribed by their class, background, and species
            foreach (var skill in charClass.Skills)
            {
                var profSkill = character.Skills.FirstOrDefault(s => s.Id == skill.Id);
                profSkill.Proficiency = true;
            }
            foreach (var skill in background.Skills)
            {
                var profSkill = character.Skills.FirstOrDefault(s => s.Id == skill.Id);
                profSkill.Proficiency = true;
            }
            foreach (var skill in species.Skills)
            {
                var profSkill = character.Skills.FirstOrDefault(s => s.Id == skill.Id);
                profSkill.Proficiency = true;
            }
            await _db.SaveChangesAsync();
            return character;
        }

        /// <summary>
        /// Deletes a specified character
        /// </summary>
        /// <param name="id">the character to be deleted</param>
        /// <returns>nothing</returns>
        public async Task DeleteAsync(int id)
        {
            Character? charToDelete = await ReadAsync(id);
            if(charToDelete != null)
            {
                _db.Characters.Remove(charToDelete);
                await _db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Reads all characters from the database
        /// </summary>
        /// <returns>a collection of all characters</returns>
        public async Task<ICollection<Character>> ReadAllAsync()
        {
            return await _db.Characters
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                .Include(c => c.Skills)
                .Include(c => c.Skills)
                    //.ThenInclude(s => s.Proficiency)
                .Include(c => c.Stats)
                    .ThenInclude(s => s.Stat)
                .Include(c => c.Stats)
                   // .ThenInclude(s => s.Score)
                .Include(c => c.Stats)
                   // .ThenInclude(s => s.Proficiency)
                .Include(c => c.Spells)
                .Include(c => c.Class)
                .Include(c => c.Background)
                .Include(c => c.Species)
                .Include(c => c.SecondClass)
                .Include(c => c.Equipment)
                    .ThenInclude(e => e.Equipment)
                .ToListAsync();
        }

        /// <summary>
        /// Reads a single character from the database
        /// </summary>
        /// <param name="id">the id of the character to be read</param>
        /// <returns>a character, or null if it wasn't found</returns>
        public async Task<Character?> ReadAsync(int id)
        {
            return await _db.Characters
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                .Include(c => c.Skills)
                .Include(c => c.Skills)
                //.ThenInclude(s => s.Proficiency)
                .Include(c => c.Stats)
                    .ThenInclude(s => s.Stat)
                .Include(c => c.Stats)
                // .ThenInclude(s => s.Score)
                .Include(c => c.Stats)
                // .ThenInclude(s => s.Proficiency)
                .Include(c => c.Spells)
                .Include(c => c.Class)
                .Include(c => c.Background)
                .Include(c => c.Species)
                .Include(c => c.SecondClass)
                .Include(c => c.Equipment)
                    .ThenInclude(e => e.Equipment)
                .FirstOrDefaultAsync(c => c.Id == id);


        }

        /// <summary>
        /// Updates a character
        /// </summary>
        /// <param name="oldId">the id of the character to be updated</param>
        /// <param name="character">the updated character</param>
        /// <returns>nothing</returns>
        public async Task UpdateAsync(int oldId, Character character)
        {
            Character? charToUpdate = await ReadAsync(oldId);
            if(charToUpdate != null)
            {

                var charClass = await _classRepo.ReadAsync(character.ClassId);
                var background = await _bgRepo.ReadAsync(character.BackgroundId);
                var species = await _speciesRepo.ReadAsync(character.SpeciesId);
                var stats = await _db.Stats.ToListAsync();
                var skills = await _db.Skills.Include(s => s.BaseStat).ToListAsync();


                //updating each value in character
                charToUpdate.Id = character.Id;
                charToUpdate.Name = character.Name;
                charToUpdate.Age = character.Age;
                charToUpdate.Alignment = character.Alignment;
                charToUpdate.ClassId = character.ClassId;
                charToUpdate.Class = charClass;
                charToUpdate.SecondClass = character.SecondClass;
                charToUpdate.SpeciesId = character.SpeciesId;
                charToUpdate.Species = species;
                charToUpdate.BackgroundId = character.BackgroundId;
                charToUpdate.Background = background;
                charToUpdate.Equipment = character.Equipment;
                charToUpdate.Spells = character.Spells;
                charToUpdate.Stats = character.Stats;
                charToUpdate.ProficiencyBonus = character.ProficiencyBonus;
                charToUpdate.Level = character.Level;

                await _db.SaveChangesAsync();
            }
        }
        
        /// <summary>
        /// Creates a PDF from a character
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns>a byte array, which corresponds to the pdf, or nothing if there isn't a character</returns>
        public async Task<byte[]> CreatePdf(int characterId)
        {
            //Reads the character, and only proceeds if it isn't null
            var character = await ReadAsync(characterId);
            if (character != null)
            {
                using var memoryStream = new MemoryStream();
                //pdf writer which writes to the memory stream
                var writer = new PdfWriter(memoryStream);
                //creating a new pdf document using the writer
                var pdf = new PdfDocument(writer);
                //setting page size
                pdf.SetDefaultPageSize(PageSize.LETTER);
                //creating new document
                var document = new Document(pdf);

                //table with 2 columns, 100% width
                var table = new Table(2);
                table.SetWidth(UnitValue.CreatePercentValue(100));

                table.AddCell("Name");
                table.AddCell(character.Name);

                table.AddCell("Age");
                table.AddCell(character.Age.ToString());

                table.AddCell("Proficiency Bonus");
                table.AddCell(character.ProficiencyBonus.ToString());

                table.AddCell("Details");
                table.AddCell($"{character.Alignment} {character.Class.Name} {character.Level}");


                table.AddCell("Species");
                table.AddCell(character.Species.Name);

                table.AddCell("Background");
                table.AddCell(character.Background.Name);

                table.SetMarginBottom(20);
                document.Add(table);

                //stats table
                document.Add(new Paragraph().Add("Stats:"));
                var statTable = new Table(3);
                statTable.SetMarginBottom(20);
                statTable.AddCell("Stat");
                statTable.AddCell("Score");
                statTable.AddCell("Proficiency");

                foreach (var stat in character.Stats)
                {
                    statTable.AddCell(stat.Stat.Name);
                    statTable.AddCell($"{stat.Score}");
                    statTable.AddCell($"{stat.Proficiency}");
                }
                document.Add(statTable);

                //skills table
                document.Add(new Paragraph().Add("Skills:"));
                var skillTable = new Table(3);
                skillTable.SetMarginBottom(20);
                skillTable.AddCell("Skill");
                skillTable.AddCell("Score");
                skillTable.AddCell("Proficiency");

                foreach (var skill in character.Skills)
                {
                    skillTable.AddCell(skill.Skill.Name);
                    skillTable.AddCell($"{skill.Score}");
                    skillTable.AddCell($"{skill.Proficiency}");
                }

                document.Add(skillTable);

                //spells table, if the character has spells
                if (character.Spells.Count < 0)
                {
                    document.Add(new Paragraph().Add("Spells:"));
                    var spellTable = new Table(4);

                    spellTable.AddCell("Name");
                    spellTable.AddCell("Description");
                    spellTable.AddCell("Dice Used");
                    spellTable.AddCell("Spell Slot Level");
                    spellTable.SetMarginBottom(20);

                    foreach(var spell in character.Spells)
                    {
                        spellTable.AddCell(spell.Spell.Name);
                        spellTable.AddCell(spell.Spell.Description);
                        if(spell.Spell.DiceDenomination != null)
                        {
                            spellTable.AddCell($"{spell.Spell.DiceToRoll}{spell.Spell.DiceDenomination.Size}");
                        }
                        else { spellTable.AddCell(""); }
                        spellTable.AddCell(spell.Spell.SlotLevel.ToString());
                    }
                    document.Add(spellTable);
                }

                //features table
                document.Add(new Paragraph().Add("Features:"));
                foreach(var feature in character.Features)
                {
                    document.Add(new Paragraph().Add(feature));
                }

                document.Add(new Paragraph().Add("Langauges:").SetMarginTop(20));
                foreach(var language in character.Languages)
                {
                    document.Add(new Paragraph().Add(language));
                }
                
                //closing the document
                document.Close();

                //and returning the memory stream w/ the document inside
                return memoryStream.ToArray();
            }
            return null;
        }

    }
}
