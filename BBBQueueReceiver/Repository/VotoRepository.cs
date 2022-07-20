namespace BBBQueueReceiver.Repository
{
    public class VotoRepository : IVotoRepository
    {
        private BbbContext _context;

        public VotoRepository(BbbContext context)
        {
            _context = context;
        }

        public IEnumerable<Voto> ListarVotos()
        {
            return _context.Votos.ToList();
        }

        public void Adicionar(Voto voto)
        {
            _context.Votos.Add(voto);
            _context.SaveChanges();
        }

    }
}
