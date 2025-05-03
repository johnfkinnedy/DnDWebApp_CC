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
    public class EquipmentController : Controller
    {
        private readonly IEquipmentRepository _equipmentRepo;
        public EquipmentController(IEquipmentRepository equipmentRepo)
        {
            _equipmentRepo = equipmentRepo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _equipmentRepo.ReadAllAsync());
        }
        [HttpGet("one/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var background = await _equipmentRepo.ReadAsync(id);
            if (background == null)
            {
                return NotFound();
            }
            return Ok(background);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromForm] Equipment equipment)
        {
            await _equipmentRepo.CreateAsync(equipment);
            return CreatedAtAction("Get", new { id = equipment.Id }, equipment);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put([FromForm] Equipment equipment)
        {
            await _equipmentRepo.UpdateAsync(equipment.Id, equipment);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteApi(int id)
        {
            await _equipmentRepo.DeleteAsync(id);
            return NoContent();
        }
       
    }

    
}
