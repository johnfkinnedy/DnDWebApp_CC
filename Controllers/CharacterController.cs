using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using iText;

namespace DnDWebApp_CC.Controllers
{
    public class CharacterController(ICharacterRepository characterRepo, ApplicationDbContext context) : Controller
    {
        private readonly ICharacterRepository _characterRepo = characterRepo;
        private readonly ApplicationDbContext _context = context;

        public async Task<IActionResult> Index()
        {
            var allCharacters = await _characterRepo.ReadAllAsync();
            return View(allCharacters);
        }

        public async Task<IActionResult> Details(int id)
        {
            var character = await _characterRepo.ReadAsync(id);
            if (character == null)
            {
                return RedirectToAction("Index");
            }
            return View(character);
        }
        public async Task<IActionResult> Create()
        {
            ViewData["allClasses"] = await _context.CharacterClasses
                .Include(c => c.Spells)
                    .ThenInclude(s => s.Spell)
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                .Include(c => c.HitDice)
                .ToListAsync();
            ViewData["allBackgrounds"] =  await _context.Backgrounds.Include(b => b.Skills).ThenInclude(s => s.Skill).ThenInclude(s => s.BaseStat).ToListAsync();
            ViewData["allSpecies"] = await _context.Species
                .Include(s => s.Skills)
                    .ThenInclude(s => s.Skill)
                .ToListAsync();
            foreach (var charClass in ViewData["AllClasses"] as List<CharacterClass>)
            {
                Console.WriteLine(charClass.Name);
                if (charClass.Spellcaster)
                {
                    Console.WriteLine("spells");
                    foreach(var spell in charClass.Spells)
                    {
                        Console.WriteLine(spell.Spell.Name);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Character newCharacter)
        {

            ViewData["allClasses"] = await _context.CharacterClasses
                .Include(c => c.Spells)
                    .ThenInclude(s => s.Spell)
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                .Include(c => c.HitDice)
                .ToListAsync();
            ViewData["allBackgrounds"] = await _context.Backgrounds.Include(b => b.Skills).ThenInclude(s => s.Skill).ThenInclude(s => s.BaseStat).ToListAsync();
            ViewData["allSpecies"] = await _context.Species
                .Include(s => s.Skills)
                    .ThenInclude(s => s.Skill)
                .ToListAsync();
            
            //no modelstate.isvalid because it won't validate the class, background, and species, even if I assign them here. idk man
                Console.WriteLine($"\n\n {newCharacter.ClassId}");
                Console.WriteLine(ModelState.IsValid);

                //await _context.Characters.AddAsync(newCharacter);
                await NewCharacter(newCharacter);
                return RedirectToAction("Index");
            

        }

        public async Task<IActionResult> Edit(int id)
        {

            ViewData["allClasses"] = await _context.CharacterClasses
                .Include(c => c.Spells)
                    .ThenInclude(s => s.Spell)
                .Include(c => c.Skills)
                    .ThenInclude(s => s.Skill)
                .Include(c => c.HitDice)
                .ToListAsync();
            ViewData["allBackgrounds"] = await _context.Backgrounds.Include(b => b.Skills).ThenInclude(s => s.Skill).ThenInclude(s => s.BaseStat).ToListAsync();
            ViewData["allSpecies"] = await _context.Species
                .Include(s => s.Skills)
                    .ThenInclude(s => s.Skill)
                .ToListAsync();
            var character = await _characterRepo.ReadAsync(id);
            if (character == null)
            {
                return RedirectToAction("Index");
            }
            return View(character);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Character character)
        {

            try
            {

                ViewData["allClasses"] = await _context.CharacterClasses
                    .Include(c => c.Spells)
                        .ThenInclude(s => s.Spell)
                    .Include(c => c.Skills)
                        .ThenInclude(s => s.Skill)
                    .Include(c => c.HitDice)
                    .ToListAsync();
                ViewData["allBackgrounds"] = await _context.Backgrounds.Include(b => b.Skills).ThenInclude(s => s.Skill).ThenInclude(s => s.BaseStat).ToListAsync();
                ViewData["allSpecies"] = await _context.Species
                    .Include(s => s.Skills)
                        .ThenInclude(s => s.Skill)
                    .ToListAsync();

                var allClasses = ViewData["allClasses"] as List<CharacterClass>;
                character.Class = allClasses!.First(c => c.Id == character.ClassId);

                var allBgs = ViewData["allBackgrounds"] as List<Background>;
                character.Background = allBgs!.First(c => c.Id == character.BackgroundId);

                var allSpecies = ViewData["allSpecies"] as List<Species>;
                character.Species = allSpecies!.First(c => c.Id == character.SpeciesId);

                //no modelstate.isvalid because it won't validate the class, background, and species, even if I assign them here. idk man

                await _characterRepo.UpdateAsync(character.Id, character);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View(character);

            }
        }

        //code adapted from: https://stackoverflow.com/questions/76576908/how-to-generate-pdf-in-net-core-using-itext7-library
        public async Task<FileContentResult> CreatePdf(int id)
        {
            var response = await _characterRepo.CreatePdf(id);
            if (response != null)
            {
                var character = await _characterRepo.ReadAsync(id);

                var stream = new MemoryStream(response);
                FileContentResult file = new FileContentResult(stream.ToArray(), "application/pdf")
                {
                    FileDownloadName = Path.GetFileName($"{character.Name}.pdf")
                };

                return file;
            }
            else return null;
        }

        public async Task<Character> NewCharacter(Character character)
        {
            await _context.Characters.AddAsync(character);
            //assigning class, background, and ID after character is added without them
            var charClass = await _context.CharacterClasses.FirstAsync(c => c.Id == character.ClassId);
            var background = await _context.Backgrounds.FirstAsync(b => b.Id == character.BackgroundId);
            var species = await _context.Species.FirstAsync(s => s.Id == character.SpeciesId);
            var stats = await _context.Stats.ToListAsync();
            var skills = await _context.Skills.Include(s => s.BaseStat).ToListAsync();

            character.Class = charClass;
            character.Background = background;
            character.Species = species;

            //populating spells if the class has them
            if (charClass.Spellcaster)
            {
                character.Spells = new List<SpellsInCharacter>();

                foreach (var spell in charClass.Spells)
                {
                    character.Spells.Add(new SpellsInCharacter
                    {
                        SpellId = spell.SpellId, Character = character
                    });
                }
            }


            //populating features
            foreach (string feature in background.Features) { character.Features.Add(feature); }
            foreach (string feature in charClass.Features) { character.Features.Add(feature); }

            //populating languages
            foreach (string language in background.Languages) { character.Languages.Add(language); }
            foreach (string language in species.Languages) { character.Languages.Add(language); }

            //adding stats, and giving them base values to be changed later (in edit screen)
            foreach (Stat stat in stats)
            {
                character.Stats.Add(new StatsInCharacter { Stat = stat, Score = 10, Proficiency = false, Character = character });
            }


            //giving character every skill. scores will be set on character update, when scores are ACTUALLY set
            foreach (Skill skill in skills)
            {
                character.Skills.Add(new SkillsInCharacter { Skill = skill, SkillId = skill.Id, Proficiency = false, Character = character, CharacterId = character.Id });
            }

            //but giving them proficiency in the skills perscribed by their class, background, and species
            foreach (var skill in charClass.Skills)
            {
                var profSkill = character.Skills.FirstOrDefault(s => s.SkillId == skill.SkillId);
                profSkill.Proficiency = true;
            }
            foreach (var skill in background.Skills)
            {
                var profSkill = character.Skills.FirstOrDefault(s => s.SkillId == skill.SkillId);
                profSkill.Proficiency = true;
            }
            foreach (var skill in species.Skills)
            {
                var profSkill = character.Skills.FirstOrDefault(s => s.SkillId == skill.SkillId);
                profSkill.Proficiency = true;
            }
            await _context.SaveChangesAsync();
            return character;
        }
    }
}
