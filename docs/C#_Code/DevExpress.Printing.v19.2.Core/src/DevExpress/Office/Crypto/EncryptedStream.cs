namespace DevExpress.Office.Crypto
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public class EncryptedStream : Stream
    {
        private ICipherProvider cipher;
        private Stream dataStream;
        private byte[] contentBuffer = new byte[0x1000];
        private long contentPosition;
        private long contentLength;
        private bool isLengthDirty;
        private bool isBufferDirty;

        public EncryptedStream(ICipherProvider cipher, Stream stream)
        {
            this.cipher = cipher;
            this.dataStream = stream;
            if (stream.Length > 0L)
            {
                stream.Position = 0L;
                this.contentLength = new BinaryReader(stream).ReadInt64();
                this.MoveToOffset(0L, true);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Flush();
            }
            base.Dispose(disposing);
        }

        public override void Flush()
        {
            if (this.isBufferDirty)
            {
                this.MoveToOffset(this.Position, true);
            }
            if (this.isLengthDirty)
            {
                if (this.Length <= 0L)
                {
                    this.dataStream.SetLength(0L);
                }
                else
                {
                    long offset = this.RoundToBlock(this.Length);
                    this.dataStream.SetLength(this.ToRealOffset(offset));
                    this.dataStream.Position = 0L;
                    BinaryWriter writer = new BinaryWriter(this.dataStream);
                    writer.Write(this.Length);
                    writer.Flush();
                }
                this.isLengthDirty = false;
            }
            this.dataStream.Flush();
        }

        private void MoveToOffset(long newPosition, bool forceRefresh)
        {
            long num = this.Position / ((long) this.contentBuffer.Length);
            long num2 = newPosition / ((long) this.contentBuffer.Length);
            if ((num != num2) | forceRefresh)
            {
                if (this.isBufferDirty)
                {
                    using (ICryptoTransform transform = this.cipher.GetEncryptor((int) num, 0))
                    {
                        transform.TransformInPlace(this.contentBuffer, 0, this.contentBuffer.Length);
                    }
                    this.dataStream.Position = this.ToRealOffset(num * this.contentBuffer.Length);
                    this.dataStream.Write(this.contentBuffer, 0, this.contentBuffer.Length);
                    this.isBufferDirty = false;
                }
                this.dataStream.Position = this.ToRealOffset(num2 * this.contentBuffer.Length);
                int destinationIndex = this.dataStream.Read(this.contentBuffer, 0, this.contentBuffer.Length);
                if (destinationIndex < this.contentBuffer.Length)
                {
                    byte[] randomBytes = DevExpress.Office.Crypto.Utils.GetRandomBytes(this.contentBuffer.Length - destinationIndex);
                    Array.Copy(randomBytes, 0, this.contentBuffer, destinationIndex, randomBytes.Length);
                }
                using (ICryptoTransform transform2 = this.cipher.GetDecryptor((int) num2, 0))
                {
                    transform2.TransformInPlace(this.contentBuffer, 0, destinationIndex);
                }
            }
            this.contentPosition = newPosition;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int num = offset;
            long num2 = Math.Max((long) 0L, (long) (this.Length - this.Position));
            count = (int) Math.Min((long) count, num2);
            for (int i = (int) (this.Position % ((long) this.contentBuffer.Length)); count > 0; i = 0)
            {
                int num4 = Math.Min(this.contentBuffer.Length - i, count);
                Buffer.BlockCopy(this.contentBuffer, i, buffer, offset, num4);
                this.MoveToOffset(this.Position + num4, false);
                offset += num4;
                count -= num4;
            }
            return (offset - num);
        }

        private long RoundToBlock(long value) => 
            (((value + this.cipher.BlockBytes) - 1L) / ((long) this.cipher.BlockBytes)) * this.cipher.BlockBytes;

        public override long Seek(long offset, SeekOrigin origin)
        {
            long num;
            if (origin == SeekOrigin.Begin)
            {
                this.Position = num = offset;
                return num;
            }
            if (origin == SeekOrigin.Current)
            {
                this.Position = num = this.Position + offset;
                return num;
            }
            if (origin != SeekOrigin.End)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.Position = num = this.Length + offset;
            return num;
        }

        private void SetContentLength(long newLength)
        {
            if (this.Length != newLength)
            {
                this.isLengthDirty = true;
                this.contentLength = newLength;
            }
        }

        public override void SetLength(long value)
        {
            long num = this.ToRealOffset(this.RoundToBlock(value));
            this.dataStream.SetLength(num);
            this.SetContentLength(value);
        }

        private long ToRealOffset(long offset) => 
            offset + 8L;

        public override void Write(byte[] buffer, int offset, int count)
        {
            for (int i = (int) (this.Position % ((long) this.contentBuffer.Length)); count > 0; i = 0)
            {
                int num2 = Math.Min(this.contentBuffer.Length - i, count);
                Buffer.BlockCopy(buffer, offset, this.contentBuffer, i, num2);
                this.isBufferDirty = true;
                this.MoveToOffset(this.Position + num2, false);
                offset += num2;
                count -= num2;
            }
            if (this.Position > this.Length)
            {
                this.SetContentLength(this.Position);
            }
        }

        public override bool CanRead =>
            this.dataStream.CanRead;

        public override bool CanSeek =>
            this.dataStream.CanSeek;

        public override bool CanWrite =>
            this.dataStream.CanWrite;

        public override long Length =>
            this.contentLength;

        public override long Position
        {
            get => 
                this.contentPosition;
            set => 
                this.MoveToOffset(value, false);
        }
    }
}

