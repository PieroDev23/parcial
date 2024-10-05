using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using parcial.Data;
using parcial.Models;

namespace parcial.Controllers
{
    public class RemittenceController : Controller
    {
        private readonly ILogger<RemittenceController> _logger;

        private readonly ApplicationDbContext _context;

        public RemittenceController(ILogger<RemittenceController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var remittances = await _context.Remittances.ToListAsync();
            return View(remittances);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Remittance remittance)
        {
            if (ModelState.IsValid)
            {
                remittance.TransactionDate = DateTime.UtcNow;
                remittance.Status = "Pending";
                _context.Remittances.Add(remittance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(remittance);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}