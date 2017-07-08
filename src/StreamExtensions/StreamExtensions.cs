using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace StreamExtensions
{
    public static class StreamExtensions
    {
        /// <summary>
        ///     Read exact amount of data from stream. 
        ///     Method if full-blocking but throws in case of any errors
        /// </summary>
        /// <param name="stream">Stream to read from</param>
        /// <param name="buffer">Buffer for read data storing</param>
        /// <param name="offset">Current offset in buffer</param>
        /// <param name="count">Number of bytes to be read</param>
        /// <exception cref="EndOfStreamException">Thrown if stream is ended before count bytes is read</exception>
        public static void ReadExact(this Stream stream, byte[] buffer, int offset, int count)
        {
            CheckParams(stream, buffer, offset, count);

            var currentOffset = offset;
            var currentRead = 0;
            while (true)
            {
                var readCount = stream.Read(buffer, currentOffset, count - currentRead);
                if (readCount == 0)
                    throw new EndOfStreamException();

                currentRead += readCount;
                if (currentRead == count)
                    return;

                currentOffset += readCount;
            }
        }

        /// <summary>
        ///     Read exact amount of data from stream as an asynchronous operation. 
        ///     Method if full-blocking but throws in case of any errors
        /// </summary>
        /// <param name="stream">Stream to read from</param>
        /// <param name="buffer">Buffer for read data storing</param>
        /// <param name="offset">Current offset in buffer</param>
        /// <param name="count">Number of bytes to be read</param>
        /// <exception cref="EndOfStreamException">Thrown if stream is ended before count bytes is read</exception>
        public static Task ReadExactAsync(this Stream stream, byte[] buffer, int offset, int count)
            => stream.ReadExactAsync(buffer, offset, count, CancellationToken.None);

        /// <summary>
        ///     Read exact amount of data from stream as an asynchronous operation. 
        ///     Method if full-blocking but throws in case of any errors
        /// </summary>
        /// <param name="stream">Stream to read from</param>
        /// <param name="buffer">Buffer for read data storing</param>
        /// <param name="offset">Current offset in buffer</param>
        /// <param name="count">Number of bytes to be read</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="EndOfStreamException">Thrown if stream is ended before count bytes is read</exception>
        public static async Task ReadExactAsync(this Stream stream, byte[] buffer, int offset, int count,
            CancellationToken cancellationToken)
        {
            CheckParams(stream, buffer, offset, count);

            var currentOffset = offset;
            var currentRead = 0;
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var readCount = await stream.ReadAsync(buffer, currentOffset, count - currentRead, cancellationToken);
                if (readCount == 0)
                    throw new EndOfStreamException();

                currentRead += readCount;
                if (currentRead == count)
                    return;

                currentOffset += readCount;
            }
        }

        private static void CheckParams(this Stream stream, byte[] buffer, int offset, int count)
        {
            if (stream == null)
                throw new ArgumentNullException();
            if (buffer == null)
                throw new ArgumentNullException();
            if (!stream.CanRead)
                throw new InvalidOperationException();

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), offset, null);
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), count, null);

            if (offset + count > buffer.Length)
                throw new InvalidOperationException("Buffer is too small");
        }
    }
}
