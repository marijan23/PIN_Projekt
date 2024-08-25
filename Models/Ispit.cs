using System.ComponentModel.DataAnnotations.Schema;

namespace PIN_Projekt.Models
{
    public class Ispit
    {
        public int Id { get; set; }
        public required int Ocijena {  get; set; }
        public required int BrojBodova { get; set; }
        public required DateTime DatumPolaganja { get; set; }

        [ForeignKey("StudentParent")]
        [Column("StudentId")]
        public int? StudentId { get; set; }
        public virtual Student? StudentParent { get; set; }


        [ForeignKey("PredmetParent")]
        [Column("PredmetId")]
        public int? PredmetId { get; set; }
        public virtual Predmet? PredmetParent { get; set; }

    }
}
