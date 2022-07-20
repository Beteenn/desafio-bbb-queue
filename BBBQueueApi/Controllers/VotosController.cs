using Microsoft.AspNetCore.Mvc;

namespace BBBQueueApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VotosController : ControllerBase
    {
        private readonly IVotoService _votoService;

        public VotosController(IVotoService votoService)
        {
            _votoService = votoService;
        }

        [HttpGet()]
        public IActionResult ListarVotos()
        {
            var votos = _votoService.ListarVotos();

            return Ok(votos);
        }

        [HttpPost()]
        public IActionResult EnviarVoto(string nomeParticipante)
        {
            _votoService.EnviarVoto(nomeParticipante);

            return Accepted("solicitacao aceita");
        }
    }
}