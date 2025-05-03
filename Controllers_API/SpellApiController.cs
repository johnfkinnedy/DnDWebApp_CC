using DnDWebApp_CC.Controllers;
using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DnDWebApp_CC.Controllers_API
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class SpellController : Controller
    {
        private readonly ISpellRepository _spellRepo;
        public SpellController(ISpellRepository spellRepo)
        {
            _spellRepo = spellRepo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _spellRepo.ReadAllAsync());
        }
        [HttpGet("one/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var background = await _spellRepo.ReadAsync(id);
            if (background == null)
            {
                return NotFound();
            }
            return Ok(background);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromForm] Spell spell)
        {
            await _spellRepo.CreateAsync(spell);
            return CreatedAtAction("Get", new { id = spell.Id }, spell);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put([FromForm] Spell spell)
        {
            await _spellRepo.UpdateAsync(spell.Id, spell);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteApi(int id)
        {
            await _spellRepo.DeleteAsync(id);
            return NoContent();
        }
       
    }

    
}
