using Marten.Schema;

namespace AspireMartenSampleWebApi
{
    [MartenDocument]
    public sealed record LargeValue
    {
        public Guid Id { get; set; }
        public UInt128 Value { get; set; } = BitConverter.ToUInt128(System.Security.Cryptography.RandomNumberGenerator.GetBytes(16));
    }
}
