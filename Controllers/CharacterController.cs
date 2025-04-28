using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DnDWebApp_CC.Controllers
{
    public class CharacterController(ICharacterRepository characterRepo, ICharacterClassRepository classRepo, IBackgroundRepository bgRepo, ISpeciesRepository speciesRepo) : Controller
    {
        private readonly ICharacterRepository _characterRepo = characterRepo;
        private readonly ICharacterClassRepository _classRepo = classRepo;
        private readonly IBackgroundRepository _bgRepo = bgRepo;
        private readonly ISpeciesRepository _speciesRepo = speciesRepo;

        public async Task<IActionResult> Index()
        {
            var allCharacters = await _characterRepo.ReadAllAsync();
            return View(allCharacters);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["allClasses"] = await _classRepo.ReadAllAsync();
            ViewData["allBackgrounds"] = await _bgRepo.ReadAllAsync();
            ViewData["allSpecies"] = await _speciesRepo.ReadAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]Character newCharacter)
        {
            if (ModelState.IsValid)
            {
                await _characterRepo.CreateAsync(newCharacter);
                return RedirectToAction("Index");
            }
            return View(newCharacter);
        }
    }
}
