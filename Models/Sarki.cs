using System.ComponentModel.DataAnnotations.Schema;

namespace AdaMuzik.Models
{
    [Table("sarkilar")]
    public class Sarki
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("ad")]
        public string Ad { get; set; }

        [Column("album_id")]
        public int AlbumId { get; set; }

        [Column("sanatci_id")]
        public int SanatciId { get; set; }

        public Album Album { get; set; }

        public Sanatci Sanatci { get; set; }

        public List<CalmaListesiSarkisi> CalmaListeleriSarkilari { get; set; }
    }
}
