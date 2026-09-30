using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    }
}