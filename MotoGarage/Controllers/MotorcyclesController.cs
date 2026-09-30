using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoGarage.Data;
using MotoGarage.Models;

namespace MotoGarage.Controllers
{
    public class MotorcyclesController : Controller
    {
        private readonly ApplicationDbContext context;

        public MotorcyclesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var motorcycles = await context.Motorcycles
                .Include(m => m.Customer)
                .ToListAsync();

            return View(motorcycles);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(
                context.Customers,
                "Id",
                "FirstName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Motorcycle motorcycle)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = new SelectList(
                    context.Customers,
                    "Id",
                    "FirstName",
                    motorcycle.CustomerId);

                return View(motorcycle);
            }

            context.Motorcycles.Add(motorcycle);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await context.Motorcycles
                .Include(m => m.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motorcycle == null)
            {
                return NotFound();
            }

            return View(motorcycle);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await context.Motorcycles.FindAsync(id);

            if (motorcycle == null)
            {
                return NotFound();
            }

            ViewBag.Customers = new SelectList(
                context.Customers,
                "Id",
                "FirstName",
                motorcycle.CustomerId);

            return View(motorcycle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
                [Bind("Id,Brand,Model,Year,EngineCapacity,Kilometers,Description,CustomerId")]
                Motorcycle motorcycle)
        {
            if (id != motorcycle.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Customers = new SelectList(
                    context.Customers,
                    "Id",
                    "FirstName",
                    motorcycle.CustomerId);

                return View(motorcycle);
            }

            context.Update(motorcycle);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await context.Motorcycles
                .Include(m => m.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motorcycle == null)
            {
                return NotFound();
            }

            return View(motorcycle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motorcycle = await context.Motorcycles.FindAsync(id);

            if (motorcycle != null)
            {
                context.Motorcycles.Remove(motorcycle);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}