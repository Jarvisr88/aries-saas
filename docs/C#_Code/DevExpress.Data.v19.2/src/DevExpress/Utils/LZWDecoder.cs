namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class LZWDecoder
    {
        private readonly Dictionary<int, byte[]> dictionary = new Dictionary<int, byte[]>();
        private readonly byte[] data;
        private readonly int initialSequenceLength;
        private readonly AddEntryMethod addEntryMethod;
        private readonly int dataSize;
        private readonly int clearTable;
        private readonly int endOfData;
        private int currentEntryLength;
        private int currentMaxEntryLength;
        private int currentDictionarySize;
        private int currentPosition;
        private byte currentSymbol;
        private int remainBits = 8;

        public LZWDecoder(byte[] data, int initialSequenceLength, bool earlyChange)
        {
            if ((initialSequenceLength < 3) || (initialSequenceLength > 9))
            {
                throw new ArgumentException("initialSequenceLength");
            }
            this.data = data;
            this.initialSequenceLength = initialSequenceLength;
            this.addEntryMethod = earlyChange ? new AddEntryMethod(this.AddEntryWithEarlyChange) : new AddEntryMethod(this.AddEntryWithLaterChange);
            this.dataSize = data.Length;
            if (this.dataSize > 0)
            {
                this.currentSymbol = data[this.currentPosition];
            }
            this.clearTable = 1 << ((initialSequenceLength - 1) & 0x1f);
            this.endOfData = this.clearTable + 1;
            this.InitializeTable();
        }

        private void AddEntry(byte[] entry)
        {
            int currentDictionarySize = this.currentDictionarySize;
            this.currentDictionarySize = currentDictionarySize + 1;
            this.dictionary.Add(currentDictionarySize, entry);
        }

        private void AddEntryWithEarlyChange(byte[] entry)
        {
            this.AddEntry(entry);
            this.EnsureEntryLength();
        }

        private void AddEntryWithLaterChange(byte[] entry)
        {
            this.EnsureEntryLength();
            this.AddEntry(entry);
        }

        public byte[] Decode()
        {
            List<byte> list = new List<byte>();
            byte[] buffer = new byte[0];
            while (true)
            {
                byte[] buffer2;
                int num = this.ReadNext();
                if (num == this.endOfData)
                {
                    return list.ToArray();
                }
                if (num == this.clearTable)
                {
                    this.InitializeTable();
                    this.dictionary.Clear();
                    buffer = new byte[0];
                    continue;
                }
                int length = buffer.Length;
                int num3 = length + 1;
                if (num < this.clearTable)
                {
                    buffer2 = new byte[] { (byte) num };
                }
                else if (num < this.currentDictionarySize)
                {
                    buffer2 = this.dictionary[num];
                }
                else
                {
                    buffer2 = new byte[num3];
                    buffer.CopyTo(buffer2, 0);
                    buffer2[length] = buffer[0];
                }
                int index = 0;
                while (true)
                {
                    if (index >= buffer2.Length)
                    {
                        if (num3 > 1)
                        {
                            byte[] array = new byte[num3];
                            buffer.CopyTo(array, 0);
                            array[length] = buffer2[0];
                            this.addEntryMethod(array);
                        }
                        buffer = buffer2;
                        break;
                    }
                    list.Add(buffer2[index]);
                    index++;
                }
            }
        }

        public static byte[] Decode(byte[] data, int initialSequenceLength) => 
            new LZWDecoder(data, initialSequenceLength, true).Decode();

        private void EnsureEntryLength()
        {
            if ((this.currentDictionarySize == this.currentMaxEntryLength) && (this.currentEntryLength < 12))
            {
                this.currentEntryLength++;
                this.currentMaxEntryLength = ((this.currentMaxEntryLength + 1) << 1) - 1;
            }
        }

        private void InitializeTable()
        {
            this.currentEntryLength = this.initialSequenceLength;
            this.currentMaxEntryLength = (1 << (this.initialSequenceLength & 0x1f)) - 1;
            this.currentDictionarySize = this.endOfData + 1;
        }

        private int ReadNext()
        {
            int num = 0;
            int num2 = this.currentEntryLength - this.remainBits;
            while (num2 > 0)
            {
                num += this.currentSymbol << (num2 & 0x1f);
                int num3 = this.currentPosition + 1;
                this.currentPosition = num3;
                if (num3 >= this.dataSize)
                {
                    return this.endOfData;
                }
                this.currentSymbol = this.data[this.currentPosition];
                this.remainBits = 8;
                num2 -= 8;
            }
            this.remainBits = -num2;
            num += this.currentSymbol >> (this.remainBits & 0x1f);
            this.currentSymbol = (byte) (this.currentSymbol & ((1 << (this.remainBits & 0x1f)) - 1));
            return num;
        }

        private delegate void AddEntryMethod(byte[] entry);
    }
}

