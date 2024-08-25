namespace PIN_Projekt.Models
{
    public class Student
    {
        public required int Id { get; set; }
        public required string Ime { get; set; }
        public required string Prezime { get; set; }
        public required string Oib { get; set; }
        public DateTime GodinaRodenja { get; set; }
        public DateTime GodinaUpisa { get; set; }
    }

}
