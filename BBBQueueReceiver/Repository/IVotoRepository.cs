namespace BBBQueueReceiver.Repository
{
    public interface IVotoRepository
    {
        IEnumerable<Voto> ListarVotos();
        void Adicionar(Voto voto);
    }
}
