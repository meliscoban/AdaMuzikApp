using AdaMuzik.Data;
using AdaMuzik.Models;
using AdaMuzik.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdaMuzik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanatcilarController : ControllerBase
    {
        private readonly AdaMuzikContext _context;

        public SanatcilarController(AdaMuzikContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Sanatci>> SanatcilariGetir()
        {
            var sanatcilar = _context.Sanatcilar.ToList();

            return Ok(sanatcilar);
        }

        [HttpPost]
        public IActionResult SanatciEkle(int albumAdet)
        {
            SanatciService ss = new SanatciService(_context);

            return Ok(ss.SanatciEkleService(albumAdet));
        }

        [HttpGet("istatistik")]
        public ActionResult<List<SanatciIstatistikDto>> SanatciBazliIstatistikAl()
        {
            SanatciService ss = new SanatciService(_context);

            return Ok(ss.SanatciBazliIstatistikAl());
        }
    }
}
