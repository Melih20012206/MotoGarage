using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoGarage.Data;

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
    }
}