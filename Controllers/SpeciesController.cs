using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Mvc;

namespace DnDWebApp_CC.Controllers
{
    public class SpeciesController(ISpeciesRepository speciesRepo) : Controller
    {
        private readonly ISpeciesRepository _speciesRepo = speciesRepo;

        public async Task<IActionResult> Index()
        {
            var allSpecies = await _speciesRepo.ReadAllAsync();
            return View(allSpecies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var species = await _speciesRepo.ReadAsync(id);
            if (species == null)
            {
                return RedirectToAction("Index");
            }
            return View(species);
        }
    }
}
