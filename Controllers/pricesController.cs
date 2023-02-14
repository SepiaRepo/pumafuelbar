using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pumafuelbar.Models;

namespace pumafuelbar.Controllers
{
    public class list{
        public double fv { get; set; }
        public double lv { get; set; }
        public string nullvalue { get; set; }
    }
    public class pricesController : Controller
    {
        private readonly dbContext _context;

        public pricesController(dbContext context)
        {
            _context = context;
        }

        // GET: prices
        public IActionResult Index()
        {
            try
            {
               
                var latestTwoPrices = _context.prices.OrderByDescending(x => x.id).Take(2).ToList();
                var latestPrice = latestTwoPrices.FirstOrDefault();
                var previousPrice = latestTwoPrices.Skip(1).FirstOrDefault();

                list obj = new list
                {
                    fv = latestPrice.rate,
                    lv = previousPrice.rate
                };

                return View(obj);
            }
            catch
            {
                string value = "value is null";
                list obj = new list()
                {
                    nullvalue = value
                };
                return View(obj);
            }


        }

        // GET: prices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.prices == null)
            {
                return NotFound();
            }

            var price = await _context.prices
                .FirstOrDefaultAsync(m => m.id == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: prices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: prices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,rate")] price price)
        {
            if (ModelState.IsValid)
            {
                _context.Add(price);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(price);
        }

        // GET: prices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.prices == null)
            {
                return NotFound();
            }

            var price = await _context.prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }
            return View(price);
        }

        // POST: prices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,rate")] price price)
        {
            if (id != price.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(price);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!priceExists(price.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(price);
        }

        // GET: prices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.prices == null)
            {
                return NotFound();
            }

            var price = await _context.prices
                .FirstOrDefaultAsync(m => m.id == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.prices == null)
            {
                return Problem("Entity set 'dbContext.prices'  is null.");
            }
            var price = await _context.prices.FindAsync(id);
            if (price != null)
            {
                _context.prices.Remove(price);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool priceExists(int id)
        {
          return (_context.prices?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
