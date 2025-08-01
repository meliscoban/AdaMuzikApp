using AdaMuzik.Data;
using AdaMuzik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;

namespace AdaMuzik.Services
{
    public class CalmaListesiService
    {
        private readonly AdaMuzikContext _context;
        private readonly Random _random = new();

        public CalmaListesiService(AdaMuzikContext context)
        {
            _context = context;
        }


        // servis 2
        public string CalmaListesiEkleService(int sarkiAdet, string ad)
        {
            List<Sarki> sarkilar = _context.Sarkilar.ToList();

            if (sarkiAdet <= 0 || sarkiAdet > sarkilar.Count)
            {
                return "Geçerli şarkı adeti giriniz.";
            }

            if (ad.IsNullOrEmpty())
            {
                return "Geçerli ad giriniz.";

            }

            CalmaListesi calmaListesi = new CalmaListesi();
            calmaListesi.Ad = ad;
            _context.CalmaListeleri.Add(calmaListesi);
            _context.SaveChanges();

            while (_context.CalmaListeleriSarkilari.Where(c => c.CalmaListesiId == calmaListesi.Id).ToList().Count != sarkiAdet)
            {
                CalmaListesiSarkisi cls = new CalmaListesiSarkisi();
                cls.CalmaListesiId = calmaListesi.Id;
                cls.SarkiId = sarkilar[_random.Next(sarkilar.Count)].Id;

                if (!_context.CalmaListeleriSarkilari.Any(c => c.SarkiId == cls.SarkiId && c.CalmaListesiId == cls.CalmaListesiId))
                {
                    _context.CalmaListeleriSarkilari.Add(cls);
                    _context.SaveChanges();
                }
            }

            return calmaListesi.Id.ToString();
        }

        // servis 3
        public void CalmaListesiYenileService(int calmaListesiId, int yeniSarkiAdet)
        {
            var eskiKayitlar = _context.CalmaListeleriSarkilari.Where(cls => cls.CalmaListesiId == calmaListesiId).ToList();

            _context.CalmaListeleriSarkilari.RemoveRange(eskiKayitlar);
            _context.SaveChanges();

            var eskiKayitSarkiIdler = eskiKayitlar.Select(cls => cls.SarkiId).ToHashSet();
            var eklenebilecekSarkilar = _context.Sarkilar.Where(s => !eskiKayitSarkiIdler.Contains(s.Id)).ToList();

            if (!_context.CalmaListeleri.Any(cl => cl.Id == calmaListesiId))
                throw new InvalidOperationException("Bu Id'ye ait çalma listesi yok.");

            if (eklenebilecekSarkilar.Count < yeniSarkiAdet || yeniSarkiAdet <= 0)
                throw new InvalidOperationException("Eklenecek yeterli saayıda şarkı yok.");


            var yeniEklenecekSarkilar = eklenebilecekSarkilar.OrderBy(x => _random.Next()).Take(yeniSarkiAdet).ToList();

            var yeniSarkilar = new List<CalmaListesiSarkisi>();

            foreach (var sarki in yeniEklenecekSarkilar)
            {
                yeniSarkilar.Add(new CalmaListesiSarkisi
                {
                    CalmaListesiId = calmaListesiId,
                    SarkiId = sarki.Id
                });
            }

            _context.CalmaListeleriSarkilari.AddRange(yeniSarkilar);
            _context.SaveChanges();
        }
    }
}
