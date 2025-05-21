using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConfectioneryManagementApp.Data;
using ConfectioneryManagementApp.Models.ViewModels;

namespace ConfectioneryManagementApp.Controllers
{
    public class CakesController : Controller
    {
        private readonly AppDbContext _context;

        public CakesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Cakes?from=2025-05-01&to=2025-05-15
        public async Task<IActionResult> Index(DateTime? from, DateTime? to)
        {
            var query = _context.Orders
                .Include(o => o.OrderedCakes)
                .AsQueryable();

            if (from.HasValue)
            {
                var fromUtc = DateTime.SpecifyKind(from.Value, DateTimeKind.Utc);
                query = query.Where(o => o.DeliveryDate >= fromUtc);
            }

            if (to.HasValue)
            {
                var toUtc = DateTime.SpecifyKind(to.Value, DateTimeKind.Utc);
                query = query.Where(o => o.DeliveryDate <= toUtc);
            }

            var result = await query
                .SelectMany(o => o.OrderedCakes.Select(c => new CakesViewModel
                {
                    Flavor = c.Flavor,
                    Size = c.Size,
                    DeliveryDate = o.DeliveryDate,
                    ClientName = o.ClientName,
                    DecorationDescription = o.DecorationDescription
                }))
                .ToListAsync();

            return View(result);
        }
    }
}