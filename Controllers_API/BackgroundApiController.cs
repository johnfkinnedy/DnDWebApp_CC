using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DnDWebApp_CC.Controllers_API
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class BackgroundController : Controller
    {
        private readonly IBackgroundRepository _bgRepo;
        public BackgroundController(IBackgroundRepository backgroundRepo)
        {
            _bgRepo = backgroundRepo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bgRepo.ReadAllAsync());
        }
        [HttpGet("/one/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var background = await _bgRepo.ReadAsync(id);
            if (background == null)
            {
                return NotFound();
            }
            return Ok(background);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromForm] Background background)
        {
            await _bgRepo.CreateAsync(background);
            return CreatedAtAction("Get", new { id = background.Id }, background);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put([FromForm] Background background)
        {
            await _bgRepo.UpdateAsync(background.Id, background);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteApi(int id)
        {
            await _bgRepo.DeleteAsync(id);
            return NoContent();
        }
       
    }

    
}
