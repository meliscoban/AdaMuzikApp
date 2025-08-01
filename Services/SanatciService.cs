using AdaMuzik.Data;
using AdaMuzik.Models;
using System;
using System.Reflection;

namespace AdaMuzik.Services
{
    public class SanatciService
    {
        private readonly AdaMuzikContext _context;
        private readonly Random _random = new();

        public SanatciService(AdaMuzikContext context)
        {
            _context = context;
        }

        // servis 1
        public int SanatciEkleService(int albumAdet)
        {
            Sanatci sanatci = new Sanatci();

            sanatci.Ad = RastgeleString(5);
            sanatci.KurulusTarihi = RastgeleTarih(new DateTime(1996, 1, 1), new DateTime(2005, 1, 1));
            _context.Sanatcilar.Add(sanatci);
            _context.SaveChanges();

            for (int i = 0; i < albumAdet; i++)
            {
                Album album = new Album();

                album.Ad = RastgeleString(10);
                album.CikisTarihi = RastgeleTarih(new DateTime(1996, 1, 1), new DateTime(2005, 1, 1));
                album.SanatciId = sanatci.Id;

                _context.Albumler.Add(album);
                _context.SaveChanges();

                int sarkiAdet = _random.Next(6, 15);

                for (int j = 0; j < sarkiAdet; j++)
                {
                    Sarki sarki = new Sarki();

                    sarki.Ad = RastgeleString(15);
                    sarki.AlbumId = album.Id;
                    sarki.SanatciId = sanatci.Id;
                    _context.Sarkilar.Add(sarki);
                    _context.SaveChanges();
                }
            }


            return sanatci.Id;
        }

        private string RastgeleString(int uzunluk)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, uzunluk)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private DateTime RastgeleTarih(DateTime baslangic, DateTime bitis)
        {
            var aralik = (bitis - baslangic).Days;
            var randomDay = _random.Next(aralik);
            var tarih = baslangic.AddDays(randomDay);

            return tarih;
        }

        // servis 4
        public List<SanatciIstatistikDto> SanatciBazliIstatistikAl()
        {
            var istatistikler = _context.Sanatcilar.Select(s => new SanatciIstatistikDto
            {
                SanatciAd = s.Ad,
                ToplamAlbumAdet = s.Albumler.Count(),
                ToplamSarkiAdet = s.Albumler.SelectMany(a => a.Sarkilar).Count()
            }).OrderByDescending(x => x.ToplamSarkiAdet).ToList();

            return istatistikler;
        }
    }
}
