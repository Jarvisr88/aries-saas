namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Text;
    using System.Text.RegularExpressions;

    public class DeflateStreamsArchiveReader : DeflateStreamsArchiveManagerBase
    {
        public DeflateStreamsArchiveReader(Stream baseStream) : base(baseStream)
        {
            if (!IsValidStream(baseStream))
            {
                ThrowInvalidOperationException();
            }
            long position = baseStream.Position;
            byte[] buffer = new byte[12];
            baseStream.Read(buffer, 0, buffer.Length);
            baseStream.Position = position + Regex.Match(new string(Encoding.UTF8.GetChars(buffer)).ToLower(), @"\d+\.\d+\.\d+\.0").Value.Length;
            base.fStreamCount = this.ReadInt32();
            base.offsets = this.ReadInt32Array(base.StreamCount);
        }

        private static bool ByteArraysEqual(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public Stream GetRawStream(int streamIndex)
        {
            base.CheckStreamIndex(streamIndex);
            base.baseStream.Seek((long) base.offsets[streamIndex], SeekOrigin.Begin);
            return base.CreateRawStream();
        }

        public Stream GetStream(int streamIndex)
        {
            base.CheckStreamIndex(streamIndex);
            base.baseStream.Seek((long) base.offsets[streamIndex], SeekOrigin.Begin);
            return base.CreateDeflateStream(CompressionMode.Decompress);
        }

        public static bool IsValidStream(Stream stream)
        {
            byte[] buffer = new byte[DeflateStreamsArchiveManagerBase.PrefixBytes.Length];
            ReadBytes(stream, buffer);
            return ByteArraysEqual(DeflateStreamsArchiveManagerBase.PrefixBytes, buffer);
        }

        private void ReadBytes(byte[] buffer)
        {
            ReadBytes(base.baseStream, buffer);
        }

        private static void ReadBytes(Stream stream, byte[] buffer)
        {
            if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                ThrowInvalidOperationException();
            }
        }

        private int ReadInt32()
        {
            byte[] buffer = new byte[4];
            this.ReadBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        private int[] ReadInt32Array(int count)
        {
            byte[] buffer = new byte[count * 4];
            this.ReadBytes(buffer);
            int[] numArray = new int[count];
            for (int i = 0; i < count; i++)
            {
                numArray[i] = BitConverter.ToInt32(buffer, i * 4);
            }
            return numArray;
        }
    }
}

