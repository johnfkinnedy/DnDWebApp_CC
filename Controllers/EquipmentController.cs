using AspNetCoreGeneratedDocument;
using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DnDWebApp_CC.Controllers
{
    public class EquipmentController(IEquipmentRepository equipmentRepo, ApplicationDbContext dbContext) : Controller
    {
        private readonly IEquipmentRepository _equipmentRepo = equipmentRepo;
        private readonly ApplicationDbContext _context = dbContext;
        public async Task<IActionResult> Index()
        {
            var allEquipment = await _equipmentRepo.ReadAllAsync();
            return View(allEquipment);
        }

        public async Task<IActionResult> Details(int id)
        {
            var equipment = await _equipmentRepo.ReadAsync(id);
            ViewData["usedByCharacters"] = await _context.EquipmentInCharacters.Where(e => e.EquipmentId == id).Include(e => e.Character).ToListAsync();
            foreach (var character in ViewData["usedByCharacters"] as List<EquipmentInCharacter>)
            {
                Console.WriteLine(character.Character.Name);
            }
            if (equipment == null)
            {
                return RedirectToAction("Index");
            }
            return View(equipment);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                await _equipmentRepo.CreateAsync(equipment);
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["allDice"] = await _context.Dice.ToListAsync();
            var background = await _equipmentRepo.ReadAsync(id);
            if (background == null)
            {
                return RedirectToAction("Index");
            }
            return View(background);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                await _equipmentRepo.UpdateAsync(equipment.Id, equipment);
                return RedirectToAction("Index");
            }
            return View(equipment);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var equipment = await _equipmentRepo.ReadAsync(id);
            if (equipment == null)
            {
                return RedirectToAction("Index");
            }
            return View(equipment);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _equipmentRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AssignCharacter([Bind(Prefix = "id")] int equipmentId, int characterId)
        {
            bool success = await _equipmentRepo.AssignCharacterAsync(equipmentId, characterId);
            if (!success)
            {
                return Json("Character could not be assigned. Sorry.");
            }
            return Json(Ok());
        }
    }
}
