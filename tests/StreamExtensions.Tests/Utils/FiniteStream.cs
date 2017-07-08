using System;

namespace StreamExtensions.Tests.Utils
{
    internal class FiniteStream : BaseTestStream
    {
        public FiniteStream(int totalBytes)
        {
            _bytesLeft = totalBytes;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var bytesToRead = Math.Min(count, _bytesLeft);
            _bytesLeft -= bytesToRead;
            for (var i = 0; i < bytesToRead; i++)
            {
                buffer[offset + i] = 0;
            }
            return bytesToRead;
        }

        public override bool CanRead => true;
        private int _bytesLeft;

    }
}
