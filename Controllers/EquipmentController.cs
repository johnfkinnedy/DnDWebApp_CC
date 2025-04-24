using Microsoft.AspNetCore.Mvc;

namespace DnDWebApp_CC.Controllers
{
    public class EquipmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
