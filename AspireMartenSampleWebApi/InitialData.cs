using Marten;
using Marten.Schema;

namespace AspireMartenSampleWebApi
{
    public class InitialData(params object[] data) : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            await using var session = store.LightweightSession();

            session.Store(data);

            await session.SaveChangesAsync();
        }
    }
}
