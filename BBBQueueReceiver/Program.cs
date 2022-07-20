using BBBQueueReceiver.Repository;
using BBBQueueReceiver.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

class Receive
{
    public static void Main()
    {
        var services = new ServiceCollection();
        ConfigurarServicos(services);
        var votoRepository = services.BuildServiceProvider().GetService<IVotoRepository>();

        Console.WriteLine("Inciado!");

        var votoService = new VotoService(votoRepository);

        votoService.ReceberVotos();
    }

    public static void ConfigurarServicos(IServiceCollection services)
    {
        services.AddTransient<IVotoService, VotoService>();
        services.AddScoped<IVotoRepository, VotoRepository>();
        services.AddDbContext<BbbContext>(opt => opt.UseInMemoryDatabase("VotosDb"));
    }
}