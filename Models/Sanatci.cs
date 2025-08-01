using System.ComponentModel.DataAnnotations.Schema;

namespace AdaMuzik.Models
{
    [Table("sanatcilar")]
    public class Sanatci
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("ad")]
        public string Ad { get; set; }

        [Column("kurulus_tarihi")]
        public DateTime KurulusTarihi { get; set; }
        
        public List<Album> Albumler { get; set; }

        public List<Sarki> Sarkilar { get; set; }
    }
}
