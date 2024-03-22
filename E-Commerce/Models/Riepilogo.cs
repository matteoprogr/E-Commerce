namespace E_Commerce.Models
{
    public class Riepilogo
    {
        public string? Nome { get; set; }
        public string? Quantità { get; set; }
        public string? Prezzo { get; set; }
        public string? TotaleProdotto { get; set; }
        public double? TotaleOrdine { get; set; }

        public Riepilogo(string? nome, string? quantità, string? prezzo, string? totaleProdotto, double? totaleOrdine)
        {
            Nome = nome;
            Quantità = quantità;
            Prezzo = prezzo;
            TotaleProdotto = totaleProdotto;
            TotaleOrdine = totaleOrdine;
        }

        public List<Riepilogo?> riepilogoList { get; set; } = new List<Riepilogo?>();

       

        public Riepilogo()
        {
        }
        
    }
}
