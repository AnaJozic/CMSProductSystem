namespace CMSProductSystem.Models
{
    public class ProizvodDetailViewModel
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Kolicina { get; set; }
        public decimal Cijena { get; set; }
        public string KategorijaNaziv { get; set; }

        public string? Slika { get; set; }
    }
}
