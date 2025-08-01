using System.ComponentModel.DataAnnotations.Schema;

namespace AdaMuzik.Models
{
    [Table("calma_listeleri")]
    public class CalmaListesi
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("ad")]
        public string Ad { get; set; }

        public List<CalmaListesiSarkisi> CalmaListeleriSarkilari { get; set; }
    }
}
