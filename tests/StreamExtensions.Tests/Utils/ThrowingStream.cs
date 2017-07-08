using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace StreamExtensions.Tests.Utils
{
    /// <summary>
    ///     Throws <see cref="IOException"/> on every read
    /// </summary>
    internal class ThrowingStream : BaseTestStream
    {
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new IOException();
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return await base.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override bool CanRead => true;
    }
}
