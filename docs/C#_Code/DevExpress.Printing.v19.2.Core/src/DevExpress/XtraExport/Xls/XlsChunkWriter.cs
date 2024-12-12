namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class XlsChunkWriter : BinaryWriter
    {
        private BinaryWriter streamWriter;
        private IXlsChunk chunk;
        private IXlsChunk continueChunk;
        private XlsChunkWriterInternalStream internalStream;
        private BinaryWriter internalWriter;
        private bool hasHighBytes;
        private long stringValuePosition;
        private long blockPosition;
        private bool suppressAutoFlush;

        public XlsChunkWriter(BinaryWriter streamWriter, IXlsChunk firstChunk, IXlsChunk continueChunk)
        {
            Guard.ArgumentNotNull(streamWriter, "streamWriter");
            Guard.ArgumentNotNull(firstChunk, "firstChunk");
            Guard.ArgumentNotNull(continueChunk, "continueChunk");
            this.streamWriter = streamWriter;
            this.chunk = firstChunk;
            this.continueChunk = continueChunk;
            this.internalStream = new XlsChunkWriterInternalStream();
            this.internalWriter = new BinaryWriter(this.internalStream);
            this.blockPosition = -1L;
            this.stringValuePosition = -1L;
        }

        public void BeginBlock()
        {
            this.EndStringValue();
            this.blockPosition = this.internalStream.Position;
        }

        public void BeginRecord(int requiredSpace)
        {
            if (this.SpaceInCurrentRecord < requiredSpace)
            {
                this.Flush();
            }
        }

        public void BeginStringValue(bool hasHighBytes)
        {
            this.EndBlock();
            this.stringValuePosition = this.internalStream.Position;
            this.hasHighBytes = hasHighBytes;
        }

        public override void Close()
        {
            this.Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Flush();
                this.FreeInternals();
                this.streamWriter = null;
                this.continueChunk = null;
                this.chunk = null;
            }
        }

        public void EndBlock()
        {
            this.blockPosition = -1L;
        }

        public void EndStringValue()
        {
            this.stringValuePosition = -1L;
            this.hasHighBytes = false;
        }

        public override void Flush()
        {
            this.suppressAutoFlush = false;
            this.internalWriter.Flush();
            this.FlushChunks();
            if (this.internalStream.Length > 0L)
            {
                this.WriteChunk();
            }
        }

        private void FlushChunks()
        {
            if (!this.suppressAutoFlush)
            {
                while (this.internalStream.Length > this.chunk.GetMaxDataSize())
                {
                    this.WriteChunk();
                }
            }
        }

        private void FreeInternals()
        {
            if (this.internalStream != null)
            {
                this.internalStream.Dispose();
                this.internalStream = null;
            }
            if (this.internalWriter != null)
            {
                this.internalWriter.Dispose();
                this.internalWriter = null;
            }
        }

        private byte[] GetChunkData()
        {
            byte[] buffer;
            int maxDataSize = this.chunk.GetMaxDataSize();
            if (this.internalStream.Length <= maxDataSize)
            {
                buffer = this.internalStream.ToArray();
                this.internalStream.RemoveBytes((int) this.internalStream.Length);
                this.EndStringValue();
                this.EndBlock();
            }
            else
            {
                int size = maxDataSize;
                if (this.blockPosition != -1L)
                {
                    size = (int) this.blockPosition;
                    if (size == 0)
                    {
                        throw new Exception("Block exceed maximum Xls record data size");
                    }
                }
                else if ((this.stringValuePosition != -1L) && this.hasHighBytes)
                {
                    int num3 = (size - ((int) this.stringValuePosition)) / 2;
                    size = ((int) this.stringValuePosition) + (num3 * 2);
                }
                buffer = this.internalStream.ToArray(size);
                if (this.stringValuePosition != -1L)
                {
                    size--;
                }
                this.internalStream.RemoveBytes(size);
                if (this.blockPosition != -1L)
                {
                    this.blockPosition = 0L;
                }
                else if (this.stringValuePosition != -1L)
                {
                    long position = this.internalStream.Position;
                    this.internalStream.Position = 0L;
                    this.internalWriter.Write(this.hasHighBytes);
                    this.stringValuePosition = 1L;
                    this.internalStream.Position = position;
                }
            }
            return buffer;
        }

        public override long Seek(int offset, SeekOrigin origin) => 
            this.internalWriter.Seek(offset, origin);

        public void SetNextChunk(IXlsChunk chunk)
        {
            this.chunk = chunk;
        }

        public override void Write(bool value)
        {
            this.internalWriter.Write(value);
            this.FlushChunks();
        }

        public override void Write(byte[] buffer)
        {
            this.internalWriter.Write(buffer);
            this.FlushChunks();
        }

        public override void Write(byte value)
        {
            this.internalWriter.Write(value);
            this.FlushChunks();
        }

        public override void Write(char ch)
        {
            this.internalWriter.Write(ch);
            this.FlushChunks();
        }

        public override void Write(char[] chars)
        {
            this.internalWriter.Write(chars);
            this.FlushChunks();
        }

        public override void Write(double value)
        {
            this.internalWriter.Write(value);
            this.FlushChunks();
        }

        public override void Write(short value)
        {
            this.internalWriter.Write(value);
            this.FlushChunks();
        }

        public override void Write(int value)
        {
            this.internalWriter.Write(value);
            this.FlushChunks();
        }

        public override void Write(long value)
        {
            this.internalWriter.Write(value);
            this.FlushChunks();
        }

        [CLSCompliant(false)]
        public override void Write(sbyte value)
        {
            this.internalWriter.Write(value);
            this.FlushChunks();
        }

        public override void Write(float value)
        {
            this.internalWriter.Write(value);
            this.FlushChunks();
        }

        public override void Write(string value)
        {
            this.internalWriter.Write(value);
            this.FlushChunks();
        }

        [CLSCompliant(false)]
        public override void Write(ushort value)
        {
            this.internalWriter.Write(value);
            this.FlushChunks();
        }

        [CLSCompliant(false)]
        public override void Write(uint value)
        {
            this.internalWriter.Write(value);
            this.FlushChunks();
        }

        [CLSCompliant(false)]
        public override void Write(ulong value)
        {
            this.internalWriter.Write(value);
            this.FlushChunks();
        }

        public override void Write(byte[] buffer, int index, int count)
        {
            this.internalWriter.Write(buffer, index, count);
            this.FlushChunks();
        }

        public override void Write(char[] chars, int index, int count)
        {
            this.internalWriter.Write(chars, index, count);
            this.FlushChunks();
        }

        private void WriteChunk()
        {
            this.chunk.Data = this.GetChunkData();
            this.chunk.Write(this.streamWriter);
            this.chunk = this.continueChunk;
        }

        public override Stream BaseStream =>
            this.internalWriter.BaseStream;

        public int SpaceInCurrentRecord =>
            this.chunk.GetMaxDataSize() - ((int) this.internalStream.Length);

        public bool SuppressAutoFlush
        {
            get => 
                this.suppressAutoFlush;
            set
            {
                if (this.suppressAutoFlush != value)
                {
                    this.suppressAutoFlush = value;
                    this.FlushChunks();
                }
            }
        }

        public int Capacity
        {
            get => 
                (int) this.internalStream.Capacity;
            set => 
                this.internalStream.Capacity = value;
        }
    }
}

