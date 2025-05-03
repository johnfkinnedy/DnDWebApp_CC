using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DnDWebApp_CC.Controllers_API
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly ICharacterClassRepository _classRepo;
        public ClassController(ICharacterClassRepository classRepo)
        {
            _classRepo = classRepo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _classRepo.ReadAllAsync());
        }
        [HttpGet("one/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var background = await _classRepo.ReadAsync(id);
            if (background == null)
            {
                return NotFound();
            }
            return Ok(background);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromForm] CharacterClass charClass)
        {
            await _classRepo.CreateAsync(charClass);
            return CreatedAtAction("Get", new { id = charClass.Id }, charClass);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put([FromForm] CharacterClass charClass)
        {
            await _classRepo.UpdateAsync(charClass.Id, charClass);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _classRepo.DeleteAsync(id);
            return NoContent();
        }
       
    }

    
}
