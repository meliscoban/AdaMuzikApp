using AdaMuzik.Data;
using AdaMuzik.Models;
using AdaMuzik.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdaMuzik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalmaListeleriController : ControllerBase
    {
        private readonly AdaMuzikContext _context;

        public CalmaListeleriController(AdaMuzikContext context)
        {
            _context = context;
        }

        [HttpPost]
        public string CalmaListesiEkle(int sarkiAdet, string ad)
        {
            CalmaListesiService ss = new CalmaListesiService(_context);

            return ss.CalmaListesiEkleService(sarkiAdet, ad);
        }

        [HttpPut]
        public IActionResult CalmaListesiYenile(int calmaListesiId, int yeniSarkiAdet)
        {
            CalmaListesiService ss = new CalmaListesiService(_context);

            ss.CalmaListesiYenileService(calmaListesiId, yeniSarkiAdet);
            return Ok("Çalma listesi yenilendi.");
        }
    }
}
