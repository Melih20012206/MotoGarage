using Microsoft.AspNetCore.Mvc;
using MotoGarage.Data;
using MotoGarage.Models;

namespace MotoGarage.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext context;

        public CustomersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            var customers = context.Customers.ToList();

            return View(customers);
        }
    }
}