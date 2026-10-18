using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoGarage.Data;
using MotoGarage.Models;

namespace MotoGarage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var model = new DashboardViewModel
            {
                CustomersCount = await context.Customers.CountAsync(),

                MotorcyclesCount = await context.Motorcycles.CountAsync(),

                ServiceRecordsCount = await context.ServiceRecords.CountAsync(),

                TodayServiceValue = await context.ServiceRecords
                    .Where(s => s.ServiceDate >= today
                        && s.ServiceDate < tomorrow)
                    .SumAsync(s => (decimal?)s.Price) ?? 0m
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}