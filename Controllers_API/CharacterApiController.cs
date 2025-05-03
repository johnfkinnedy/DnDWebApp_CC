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
    public class CharacterController : Controller
    {
        private readonly ICharacterRepository _characterRepo;
        public CharacterController(ICharacterRepository characterRepo)
        {
            _characterRepo = characterRepo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _characterRepo.ReadAllAsync());
        }
        [HttpGet("one/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var background = await _characterRepo.ReadAsync(id);
            if (background == null)
            {
                return NotFound();
            }
            return Ok(background);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromForm] Character character)
        {
            await _characterRepo.CreateAsync(character);
            return CreatedAtAction("Get", new { id = character.Id }, character);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put([FromForm] Character character)
        {
            await _characterRepo.UpdateAsync(character.Id, character);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteApi(int id)
        {
            await _characterRepo.DeleteAsync(id);
            return NoContent();
        }
       
    }

    
}
