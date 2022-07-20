using System.Text;
using BBBQueueApi.Repositories;
using Newtonsoft.Json;
using RabbitMQ.Client;

public class VotoService : IVotoService
{
    private readonly IVotoRepository _votoRepository;
    private readonly ConnectionFactory _factory;

    public VotoService(IVotoRepository votoRepository)
    {
        _votoRepository = votoRepository;
        _factory = new ConnectionFactory() { HostName = "localhost" };
    }

    public IEnumerable<Voto> ListarVotos()
    {
        return _votoRepository.ListarVotos();
    }

    public void EnviarVoto(string nomeParticipante)
    {
        using (var connection = _factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "votos",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = PrepararVotoParaEnvio(nomeParticipante);

            channel.BasicPublish(exchange: "",
                routingKey: "votos",
                basicProperties: null,
                body: body);
        }
    }

    private byte[] PrepararVotoParaEnvio(string nomeParticipante)
    {
        var votoDto = new VotoDto { Id = Guid.NewGuid(), NomeParticipante = nomeParticipante, DataVoto = DateTime.Now };

        var votoJson = JsonConvert.SerializeObject(votoDto);

        return Encoding.UTF8.GetBytes(votoJson);
    }
}