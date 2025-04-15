using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Mvc;

namespace DnDWebApp_CC.Controllers
{
    public class BackgroundController : Controller
    {
        private readonly IBackgroundRepository _bgRepo;
        public BackgroundController(IBackgroundRepository backgroundRepo)
        {
            _bgRepo = backgroundRepo;
        }
        public async Task<IActionResult> Index()
        {
            var allBackgrounds = await _bgRepo.ReadAllAsync();
            return View(allBackgrounds);
        }
    }
}
