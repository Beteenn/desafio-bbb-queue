using BBBQueueReceiver.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BBBQueueReceiver.Services
{
    public class VotoService : IVotoService
    {
        private readonly IVotoRepository _votoRepository;
        private readonly ConnectionFactory _factory;

        public VotoService(IVotoRepository votoRepository)
        {
            _votoRepository = votoRepository;
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public void ReceberVotos()
        {
            
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "votos",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    SalvarVoto(ea);
                    ListarVotosBanco();
                };

                channel.BasicConsume(queue: "votos",
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine("Pressione alguma tecla para encerar.");
                Console.ReadLine();
            }
        }

        private void SalvarVoto(BasicDeliverEventArgs envento)
        {
            var voto = TratarRetornoFila(envento.Body.ToArray());

            _votoRepository.Adicionar(voto);

            Console.WriteLine($"[X] Voto Recebido para {voto.NomeParticipante}");
            Console.WriteLine("\n");
        }

        private Voto TratarRetornoFila(byte[] mensagemArray)
        {
            var mensagem = Encoding.UTF8.GetString(mensagemArray);

            var voto = Newtonsoft.Json.JsonConvert.DeserializeObject<Voto>(mensagem)!;

            voto.AdicionarDataVotoComputado();

            return voto;
        }

        private void ListarVotosBanco()
        {
            var votos = _votoRepository.ListarVotos();

            Console.WriteLine("Votos já Computados:");

            int counter = 1;

            foreach (var votoDb in votos)
            {
                Console.WriteLine($"{counter} - {JsonConvert.SerializeObject(votoDb)}");
                counter++;
            }

            Console.WriteLine("\n");
        }
    }
}
