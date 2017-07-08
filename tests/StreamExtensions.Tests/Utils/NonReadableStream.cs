using System;

namespace StreamExtensions.Tests.Utils
{
    /// <summary>
    ///     Read is not supported on this stream
    /// </summary>
    internal class NonReadableStream : BaseTestStream
    {
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException(nameof(NonReadableStream));
        }

        public override bool CanRead => false;
    }
}
