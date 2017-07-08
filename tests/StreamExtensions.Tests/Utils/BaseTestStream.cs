using System;
using System.IO;

namespace StreamExtensions.Tests.Utils
{
    internal abstract class BaseTestStream : Stream
    {
        #region reading

        #endregion

        #region unused
        
        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
       
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => 1;

        public override long Position
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        #endregion
    }
}
