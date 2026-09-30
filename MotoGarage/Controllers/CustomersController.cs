using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            var customers = await context.Customers
                .ToListAsync();

            return View(customers);
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await context.Customers
                .Include(c => c.Motorcycles)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await context.Customers
                .Include(c => c.Motorcycles)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            if (customer.Motorcycles.Any())
            {
                TempData["ErrorMessage"] =
                    "You cannot delete this customer because they still have motorcycles.";

                return RedirectToAction(nameof(Index));
            }

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,FirstName,LastName,PhoneNumber,Email")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            context.Update(customer);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}