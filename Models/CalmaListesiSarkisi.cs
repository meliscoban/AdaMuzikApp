using System.ComponentModel.DataAnnotations.Schema;

namespace AdaMuzik.Models
{
    [Table("calma_listeleri_sarkilari")]
    public class CalmaListesiSarkisi
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("calma_listesi_id")]
        public int CalmaListesiId { get; set; }

        [Column("sarki_id")]
        public int SarkiId { get; set; }

        public CalmaListesi CalmaListesi { get; set; }
        public Sarki Sarki { get; set; }
    }
}
