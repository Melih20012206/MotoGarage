using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoGarage.Data;
using MotoGarage.Models;

namespace MotoGarage.Controllers
{
    public class ServiceRecordsController : Controller
    {
        private readonly ApplicationDbContext context;

        public ServiceRecordsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var records = await context.ServiceRecords
                .Include(s => s.Motorcycle)
                .OrderByDescending(s => s.ServiceDate)
                .ToListAsync();

            return View(records);
        }

        public async Task<IActionResult> Create()
        {
            await LoadMotorcycles();

            return View(new ServiceRecord
            {
                ServiceDate = DateTime.Today
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ServiceType,ServiceDate,Price,Notes,MotorcycleId")]
            ServiceRecord record)
        {
            bool motorcycleExists = await context.Motorcycles
                .AnyAsync(m => m.Id == record.MotorcycleId);

            if (!motorcycleExists)
            {
                ModelState.AddModelError(
                    nameof(ServiceRecord.MotorcycleId),
                    "Please select an existing motorcycle.");
            }

            if (!ModelState.IsValid)
            {
                await LoadMotorcycles(record.MotorcycleId);
                return View(record);
            }

            context.ServiceRecords.Add(record);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadMotorcycles(int? selectedId = null)
        {
            var motorcycles = await context.Motorcycles
                .OrderBy(m => m.Brand)
                .ThenBy(m => m.Model)
                .Select(m => new
                {
                    m.Id,
                    DisplayName = m.Brand + " " + m.Model
                        + " (" + m.Year + ") — #" + m.Id
                })
                .ToListAsync();

            ViewBag.Motorcycles = new SelectList(
                motorcycles,
                "Id",
                "DisplayName",
                selectedId);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var record = await context.ServiceRecords.FindAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            await LoadMotorcycles(record.MotorcycleId);
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,ServiceType,ServiceDate,Price,Notes,MotorcycleId")]
    ServiceRecord record)
        {
            if (id != record.Id)
            {
                return NotFound();
            }

            var existingRecord = await context.ServiceRecords.FindAsync(id);

            if (existingRecord == null)
            {
                return NotFound();
            }

            bool motorcycleExists = await context.Motorcycles
                .AnyAsync(m => m.Id == record.MotorcycleId);

            if (!motorcycleExists)
            {
                ModelState.AddModelError(
                    nameof(ServiceRecord.MotorcycleId),
                    "Please select an existing motorcycle.");
            }

            if (!ModelState.IsValid)
            {
                await LoadMotorcycles(record.MotorcycleId);
                return View(record);
            }

            existingRecord.ServiceType = record.ServiceType;
            existingRecord.ServiceDate = record.ServiceDate;
            existingRecord.Price = record.Price;
            existingRecord.Notes = record.Notes;
            existingRecord.MotorcycleId = record.MotorcycleId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var record = await context.ServiceRecords
                .Include(s => s.Motorcycle)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var record = await context.ServiceRecords.FindAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            context.ServiceRecords.Remove(record);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}