using Projects;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var postgres = builder.AddPostgres("postgres").WithDataVolume(isReadOnly: false);
        var marten = postgres.AddDatabase(name: "marten", databaseName: "marten");

        builder.AddProject<AspireMartenSampleWebApi>("api").WithReference(marten).WaitFor(marten).WithExternalHttpEndpoints();

        builder.Build().Run();
    }
}