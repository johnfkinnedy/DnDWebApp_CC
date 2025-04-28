using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DnDWebApp_CC.Controllers
{
    public class SpellController(ISpellRepository spellRepo, IDiceRepository diceRepo, ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ISpellRepository _spellRepo = spellRepo;
        private readonly IDiceRepository _diceRepo = diceRepo;

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
        public async Task<IActionResult> Create()
        {
            //context is used so dice is tracked along with spell; using repos complicates things (it tries to make a new dice each time)
            ViewData["allDice"] = await _context.Dice.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Spell spell)
        {
            if (ModelState.IsValid)
            {
                //adding the spell,
                await _context.Spells.AddAsync(spell);
                //and saving changes
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spell);
        }
        //new page to test rolls for spells
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
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["allDice"] = await _context.Dice.ToListAsync();
            var spell = await _spellRepo.ReadAsync(id);
            if (spell == null)
            {
                return RedirectToAction("Index");
            }
            return View(spell);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Spell spell)
        {
            if (ModelState.IsValid)
            {
                await _spellRepo.UpdateAsync(spell.Id, spell);
                return RedirectToAction("Index");
            }
            return View(spell);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var spell = await _spellRepo.ReadAsync(id);
            if (spell == null)
            {
                return RedirectToAction("Index");
            }
            return View(spell);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _spellRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
