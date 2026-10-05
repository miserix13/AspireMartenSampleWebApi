using Marten;
using Marten.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace AspireMartenSampleWebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
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

                var app = builder.Build();

                app.MapGet("/", async (HttpContext context) =>
                {
                    await using IDocumentSession session = context.RequestServices.DocumentStore().LightweightSession();

                    await session.Query<LargeValue>().WriteArray(context);
                });

                app.MapGet("/add", async (HttpContext context) =>
                {
                    List<LargeValue> values = [];

                    for (int i = 0; i < 23; i++)
                    {
                        values.Add(new());
                    }

                    await using IDocumentSession session = context.RequestServices.DocumentStore().LightweightSession();

                    session.Store<LargeValue>(values);

                    await session.SaveChangesAsync();

                    return values.Count;
                });

                await app.StartAsync();

                await app.WaitForShutdownAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("{Message}");
            }
        }
    }
}
