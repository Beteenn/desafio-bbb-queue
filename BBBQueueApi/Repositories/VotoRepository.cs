namespace BBBQueueApi.Repositories
{
    public class VotoRepository : IVotoRepository
    {
        private readonly BbbContext _context;

        public VotoRepository(BbbContext context)
        {
            _context = context;
        }

        public IEnumerable<Voto> ListarVotos()
        {
            return _context.Votos.ToList();
        }
    }
}
