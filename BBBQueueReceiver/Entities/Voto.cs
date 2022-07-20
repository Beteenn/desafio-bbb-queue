public class Voto
{
    public Guid Id { get; set; }    
    public string NomeParticipante { get; set; }
    public DateTime DataVoto { get; set; }
    public DateTime DataVotoComputado { get; private set; }

    public void AdicionarDataVotoComputado()
    {
        DataVotoComputado = DateTime.Now;
    }
}