using System.Text.Json;
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
            if (!ModelState.IsValid)
            {
                return View(remittance);
            }

            if (remittance.Currency.Trim().ToLower() == "usd")
            {
                remittance.ExchangeRate = 1;
                remittance.FinalAmount = remittance.AmountSent;
            }

            if (remittance.Currency.Trim().ToLower() == "bitcoin")
            {
                var btcRate = await GetBTCToUSDExchangeRate();
                remittance.ExchangeRate = btcRate;
                remittance.FinalAmount = remittance.AmountSent * btcRate;
            }

            remittance.Status = "Pending";
            remittance.TransactionDate = DateTime.UtcNow;

            _context.Remittances.Add(remittance);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index)); ;
        }

        public async Task<decimal> GetBTCToUSDExchangeRate()
        {
            var client = new HttpClient();
            var response = await client.GetStreamAsync("https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd");
            var price = await JsonSerializer.DeserializeAsync<Dictionary<string, Dictionary<string, decimal>>>(response);

            return price!["bitcoin"]["usd"];
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}