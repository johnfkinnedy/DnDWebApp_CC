using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DnDWebApp_CC.Controllers
{
    public class SpellController(ISpellRepository spellRepo) : Controller
    {
        private readonly ISpellRepository _spellRepo = spellRepo;

        public async Task<IActionResult> Index()
        {
            var allSpells = await _spellRepo.ReadAllAsync();
            return View(allSpells);
        }

        public async Task<IActionResult> Details(int id)
        {
            var spell = await _spellRepo.ReadAsync(id);
            if (spell == null)
            {
                return RedirectToAction("Index");
            }
            return View(spell);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Spell spell)
        {
            if (ModelState.IsValid)
            {
                await _spellRepo.CreateAsync(spell);
                return RedirectToAction(nameof(Index));
            }
            return View(spell);
        }

        public async Task<IActionResult> Roll(int id)
        {
            Spell? spell = await _spellRepo.ReadAsync(id);
            if(spell == null)
            {
                return RedirectToAction("Details", id);
            }
            return View(spell);
        }
        
        public async Task<IActionResult> GetOneJson(int id)
        {
            var spell = await _spellRepo.ReadAsync(id);
            return Json(spell);
        }
    }
}
