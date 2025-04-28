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

        [HttpGet("/all")]
        public async Task<IActionResult> GetAll()
        {
            var backgrounds = await _bgRepo.ReadAllAsync();
            return Ok(backgrounds);    
        }
       
    }

    
}
