using System.ComponentModel.DataAnnotations.Schema;

namespace AdaMuzik.Models
{
    [Table("albumler")]
    public class Album
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("ad")]
        public string Ad { get; set; }

        [Column("cikis_tarihi")]
        public DateTime CikisTarihi { get; set; }

        [Column("sanatci_id")]
        public int SanatciId { get; set; }

        public Sanatci Sanatci { get; set; }

        public List<Sarki> Sarkilar { get; set; }
    }
}
