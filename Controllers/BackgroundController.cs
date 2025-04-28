using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DnDWebApp_CC.Models.Entities;
using DnDWebApp_CC.Services;
using System.Net.Http;

namespace DnDWebApp_CC.Controllers
{
    public class BackgroundController(IBackgroundRepository bgRepo) : Controller
    {
        private readonly IBackgroundRepository _bgRepo = bgRepo;
        public async Task<IActionResult> Index()
        {
            var backgrounds = await _bgRepo.ReadAllAsync();
            return View(backgrounds);
        }
        public async Task<IActionResult> Details(int id)
        {
            var Background = await _bgRepo.ReadAsync(id);
            if (Background == null)
            {
                return RedirectToAction("Index");
            }
            return View(Background);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Background background)
        {
            if (ModelState.IsValid)
            {
                await _bgRepo.CreateAsync(background);
                return RedirectToAction(nameof(Index));
            }
            return View(background);
        }
        //new page to test rolls for Backgrounds
        public async Task<IActionResult> Roll(int id)
        {
            Background? Background = await _bgRepo.ReadAsync(id);
            if (Background == null)
            {
                return RedirectToAction("Details", id);
            }
            return View(Background);
        }

        public async Task<IActionResult> GetOneJson(int id)
        {
            var Background = await _bgRepo.ReadAsync(id);
            return Json(Background);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Background = await _bgRepo.ReadAsync(id);
            if (Background == null)
            {
                return RedirectToAction("Index");
            }
            return View(Background);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Background Background)
        {
            if (ModelState.IsValid)
            {
                await _bgRepo.UpdateAsync(Background.Id, Background);
                return RedirectToAction("Index");
            }
            return View(Background);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var Background = await _bgRepo.ReadAsync(id);
            if (Background == null)
            {
                return RedirectToAction("Index");
            }
            return View(Background);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bgRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
