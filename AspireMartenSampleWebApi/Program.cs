using Marten;
using Microsoft.AspNetCore.Mvc;

namespace AspireMartenSampleWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using ILoggerFactory factory = LoggerFactory.Create(b => b.AddConsole());
            ILogger<Program> logger = factory.CreateLogger<Program>();

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Logging.AddConsole();

                builder.AddServiceDefaults();

                builder.AddKeyedNpgsqlDataSource("marten");

                builder.Services.AddMarten().UseNpgsqlDataSource(serviceKey: "marten").UseLightweightSessions().InitializeWith(new InitialData(InitialDataSet.LargeValues));

                
            }
            catch (Exception ex)
            {
                logger.LogError("{Message}");
            }
        }
    }
}
