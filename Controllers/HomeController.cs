using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DnDWebApp_CC.Models;
using DnDWebApp_CC.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApp_CC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        //passing in counts for all major tables to be shown on homepage
        ViewData["characterCount"] = await _db.Characters.CountAsync();
        ViewData["speciesCount"] = await _db.Species.CountAsync();
        ViewData["classCount"] = await _db.CharacterClasses.CountAsync();
        ViewData["backgroundCount"] = await _db.Backgrounds.CountAsync();
        ViewData["spellCount"] = await _db.Spells.CountAsync();
        ViewData["classCount"] = await _db.CharacterClasses.CountAsync();
        ViewData["equipmentCount"] = await _db.Equipment.CountAsync();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
