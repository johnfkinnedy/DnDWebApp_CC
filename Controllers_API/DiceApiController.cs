using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DnDWebApp_CC.Controllers_API
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class DiceController : Controller
    {
        private readonly IDiceRepository _diceRepo;
        public DiceController(IDiceRepository dcieRepo)
        {
            _diceRepo = dcieRepo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _diceRepo.ReadAllAsync());
        }
        [HttpGet("one/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var background = await _diceRepo.ReadAsync(id);
            if (background == null)
            {
                return NotFound();
            }
            return Ok(background);
        }

       
    }

    
}
