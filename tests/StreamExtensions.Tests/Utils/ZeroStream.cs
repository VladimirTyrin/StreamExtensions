namespace StreamExtensions.Tests.Utils
{
    /// <summary>
    ///     Always readable, returns zeros
    /// </summary>
    internal class ZeroStream : BaseTestStream
    {
        public override int Read(byte[] buffer, int offset, int count)
        {
            for (var i = 0; i < count; i++)
            {
                buffer[offset + i] = 0;
            }
            return count;
        }

        public override bool CanRead => true;
    }
}
