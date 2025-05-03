using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DnDWebApp_CC.Controllers_API
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : Controller
    {
        private readonly ISpeciesRepository _speciesRepo;
        public SpeciesController(ISpeciesRepository speciesRepo)
        {
            _speciesRepo = speciesRepo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _speciesRepo.ReadAllAsync());
        }
        [HttpGet("one/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var background = await _speciesRepo.ReadAsync(id);
            if (background == null)
            {
                return NotFound();
            }
            return Ok(background);
        }

       
    }

    
}
