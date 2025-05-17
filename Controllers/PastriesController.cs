using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConfectioneryManagementApp.Data;
using ConfectioneryManagementApp.Models.ViewModels;

namespace ConfectioneryManagementApp.Controllers
{
    public class PastriesController : Controller
    {
        private readonly AppDbContext _context;

        public PastriesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? from, DateTime? to)
        {
            var query = _context.Orders
                .Include(o => o.Cakes) // to są "PastryEntity"
                .AsQueryable();

            if (from.HasValue)
                query = query.Where(o => o.DeliveryDate >= from.Value);

            if (to.HasValue)
                query = query.Where(o => o.DeliveryDate <= to.Value);

            var result = await query
                .SelectMany(o => o.Cakes)
                .GroupBy(p => p.Name)
                .Select(g => new PastrySummaryViewModel
                {
                    Name = g.Key,
                    Quantity = g.Count()
                })
                .ToListAsync();

            return View(result);
        }
    }
}