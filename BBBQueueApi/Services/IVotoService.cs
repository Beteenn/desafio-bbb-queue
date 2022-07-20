public interface IVotoService
{
    IEnumerable<Voto> ListarVotos();
    void EnviarVoto(string nomeParticipante);
}