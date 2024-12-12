namespace DevExpress.Utils.StructuredStorage.Internal.Reader
{
    using DevExpress.Utils.StructuredStorage.Internal;
    using System;
    using System.IO;

    [CLSCompliant(false)]
    public class InputHandler : AbstractIOHandler
    {
        private const int HeaderSector = -1;

        public InputHandler(Stream stream) : base(stream)
        {
        }

        internal void Read(byte[] array)
        {
            this.Read(array, 0, array.Length);
        }

        internal void Read(byte[] array, int offset, int count)
        {
            if (base.Stream.Read(array, offset, count) != count)
            {
                this.ThrowReadBytesAmountMismatchException();
            }
        }

        internal byte ReadByte()
        {
            int num = base.Stream.ReadByte();
            if (num == -1)
            {
                this.ThrowReadBytesAmountMismatchException();
            }
            return (byte) num;
        }

        internal void ReadPosition(byte[] array, long position)
        {
            if (position < 0L)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            base.Stream.Seek(position, SeekOrigin.Begin);
            if (base.Stream.Read(array, 0, array.Length) != array.Length)
            {
                this.ThrowReadBytesAmountMismatchException();
            }
        }

        internal string ReadString(int size)
        {
            if (base.BitConverter == null)
            {
                this.ThrowFileHandlerNotCorrectlyInitializedException();
            }
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException("size");
            }
            byte[] array = new byte[size];
            this.Read(array);
            return base.BitConverter.ToString(array);
        }

        internal ushort ReadUInt16()
        {
            if (base.BitConverter == null)
            {
                this.ThrowFileHandlerNotCorrectlyInitializedException();
            }
            byte[] array = new byte[2];
            this.Read(array);
            return base.BitConverter.ToUInt16(array);
        }

        internal ushort ReadUInt16(long position)
        {
            if (base.BitConverter == null)
            {
                this.ThrowFileHandlerNotCorrectlyInitializedException();
            }
            if (position < 0L)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            byte[] array = new byte[2];
            this.ReadPosition(array, position);
            return base.BitConverter.ToUInt16(array);
        }

        internal uint ReadUInt32()
        {
            if (base.BitConverter == null)
            {
                this.ThrowFileHandlerNotCorrectlyInitializedException();
            }
            byte[] array = new byte[4];
            this.Read(array);
            return base.BitConverter.ToUInt32(array);
        }

        internal uint ReadUInt32(long position)
        {
            if (base.BitConverter == null)
            {
                this.ThrowFileHandlerNotCorrectlyInitializedException();
            }
            if (position < 0L)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            byte[] array = new byte[4];
            this.ReadPosition(array, position);
            return base.BitConverter.ToUInt32(array);
        }

        internal ulong ReadUInt64()
        {
            if (base.BitConverter == null)
            {
                this.ThrowFileHandlerNotCorrectlyInitializedException();
            }
            byte[] array = new byte[8];
            this.Read(array);
            return base.BitConverter.ToUInt64(array);
        }

        internal ulong ReadUInt64(long position)
        {
            if (base.BitConverter == null)
            {
                this.ThrowFileHandlerNotCorrectlyInitializedException();
            }
            if (position < 0L)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            byte[] array = new byte[8];
            this.ReadPosition(array, position);
            return base.BitConverter.ToUInt64(array);
        }

        internal long SeekToPositionInSector(long sector, long position)
        {
            if (base.Header == null)
            {
                this.ThrowFileHandlerNotCorrectlyInitializedException();
            }
            if ((position < 0L) || (position >= base.Header.SectorSize))
            {
                throw new ArgumentOutOfRangeException("position");
            }
            return ((sector != -1L) ? base.Stream.Seek(((sector << (base.Header.SectorShift & 0x3f)) + 0x200L) + position, SeekOrigin.Begin) : base.Stream.Seek(position, SeekOrigin.Begin));
        }

        internal long SeekToSector(long sector)
        {
            if (base.Header == null)
            {
                this.ThrowFileHandlerNotCorrectlyInitializedException();
            }
            if (sector < 0L)
            {
                throw new ArgumentOutOfRangeException("sector");
            }
            return ((sector != -1L) ? base.Stream.Seek((sector << (base.Header.SectorShift & 0x3f)) + 0x200L, SeekOrigin.Begin) : base.Stream.Seek(0L, SeekOrigin.Begin));
        }

        private void ThrowFileHandlerNotCorrectlyInitializedException()
        {
            throw new Exception("The file handler is not correctly initialized.");
        }

        private void ThrowReadBytesAmountMismatchException()
        {
            throw new Exception("The number of bytes read mismatches the specified amount.");
        }

        internal int UncheckedRead(byte[] array, int offset, int count) => 
            base.Stream.Read(array, offset, count);

        public override ulong IOStreamSize =>
            (ulong) base.Stream.Length;
    }
}

