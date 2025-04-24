using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DnDWebApp_CC.Controllers
{
    public class CharacterClassController(ICharacterClassRepository classRepo) : Controller
    {
        private readonly ICharacterClassRepository _classRepo = classRepo;
        public async Task<IActionResult> Index()
        {
            var allClasses = await _classRepo.ReadAllAsync();
            return View(allClasses);
        }
    }
}
