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
    public class BackgroundController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;


        public BackgroundController(ApplicationDbContext context)
        {
            _context = context;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7130/api/background");
        }

        // GET: Backgrounds
        public async Task<IActionResult> Index()
        {
            var backgrounds = await _httpClient.GetFromJsonAsync<List<Background>>("/all"); ;
            return View(backgrounds);
        }

        // GET: Backgrounds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var background = await _context.Backgrounds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (background == null)
            {
                return NotFound();
            }

            return View(background);
        }

        // GET: Backgrounds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Backgrounds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Features,Languages")] Background background)
        {
            if (ModelState.IsValid)
            {
                _context.Add(background);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(background);
        }

        // GET: Backgrounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var background = await _context.Backgrounds.FindAsync(id);
            if (background == null)
            {
                return NotFound();
            }
            return View(background);
        }

        // POST: Backgrounds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Features,Languages")] Background background)
        {
            if (id != background.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(background);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BackgroundExists(background.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(background);
        }

        // GET: Backgrounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var background = await _context.Backgrounds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (background == null)
            {
                return NotFound();
            }

            return View(background);
        }

        // POST: Backgrounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var background = await _context.Backgrounds.FindAsync(id);
            if (background != null)
            {
                _context.Backgrounds.Remove(background);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BackgroundExists(int id)
        {
            return _context.Backgrounds.Any(e => e.Id == id);
        }
    }
}
