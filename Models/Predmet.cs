using System.ComponentModel.DataAnnotations.Schema;

namespace PIN_Projekt.Models
{
    public class Predmet
    {
        public int Id { get; set; }
        public required string Ime { get; set; }

        [ForeignKey("SmjerParent")]
        [Column("SmjerId")]
        public int? SmjerId { get; set; }

        public virtual Smjer? SmjerParent { get; set; }
    }
}
