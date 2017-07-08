using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StreamExtensions.Tests.Utils;

namespace StreamExtensions.Tests
{
    [TestClass]
    public class StreamExtensionsTests
    {
        #region bad arguments
            
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadExact_ShouldThrowIfStreamIsNull()
        {
            Stream nullStream = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            nullStream.ReadExact(Buffer, 0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ReadExactAsync_ShouldThrowIfStreamIsNull()
        {
            Stream nullStream = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            await nullStream.ReadExactAsync(Buffer, 0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadExact_ShouldThrowIfBufferIsNull()
        {
            var stream = new ZeroStream();
            stream.ReadExact(null, 0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadExact_ShouldThrowIfStreamIsNonReadable()
        {
            var stream = new NonReadableStream();
            stream.ReadExact(Buffer, 0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadExact_ShouldThrowIfOffsetIsNegative()
        {
            var stream = new ZeroStream();
            stream.ReadExact(Buffer, -1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadExact_ShouldThrowIfCountIsNegative()
        {
            var stream = new ZeroStream();
            stream.ReadExact(Buffer, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadExact_ShouldThrowIfBufferIsTooSmall()
        {
            var stream = new ZeroStream();
            stream.ReadExact(Buffer, 1, Buffer.Length);
        }

        #endregion

        #region runtime errors

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public async Task ReadExactAsync_ShouldThrowIfCancellationIsRequested()
        {
            using (var cts = new CancellationTokenSource())
            {
                cts.Cancel();
                var stream = new ZeroStream();
                await stream.ReadExactAsync(Buffer, 0, 1, cts.Token);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void ReadExact_ShouldThrowInCaseOfReadingError()
        {
            var stream = new ThrowingStream();
            stream.ReadExact(Buffer, 0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public async Task ReadExactAsync_ShouldThrowInCaseOfReadingError()
        {
            var stream = new ThrowingStream();
            await stream.ReadExactAsync(Buffer, 0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadExact_ShouldThrowInCaseOfStreamEnd()
        {
            var stream = new FiniteStream(10);
            stream.ReadExact(Buffer, 0, 20);
        }

        #endregion

        #region good read

        [TestMethod]
        public void ReadExact_ShouldSucceedIfEverythingIsOk()
        {
            var stream = new SequentialStream();
            stream.ReadExact(Buffer, 0, 100);
            for (var i = 0; i < 100; i++)
            {
                Assert.AreEqual(i, Buffer[i]);
            }
        }

        #endregion

        #region utils

        private static readonly byte[] Buffer = new byte[1024];

        #endregion
    }
}
