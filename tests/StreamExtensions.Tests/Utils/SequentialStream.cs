namespace StreamExtensions.Tests.Utils
{
    /// <summary>
    ///     Returns one byte each Read call. Bytes have incrementing values
    /// </summary>
    internal class SequentialStream : BaseTestStream
    {
        public override int Read(byte[] buffer, int offset, int count)
        {
            buffer[offset] = _position;
            _position++;
            return 1;
        }

        public override bool CanRead => true;

        private byte _position;
    }
}
