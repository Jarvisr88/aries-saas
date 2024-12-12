namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using System.Text;

    public class ChunkedStringBuilder : IStringBuilder
    {
        public const int DefaultMaxBufferSize = 0x2000;
        private int maxBufferSize;
        private List<StringBuilder> buffers;
        private int totalLength;

        public ChunkedStringBuilder()
        {
            this.maxBufferSize = 0x2000;
            this.buffers = new List<StringBuilder>();
            this.Initialize();
        }

        public ChunkedStringBuilder(string value)
        {
            this.maxBufferSize = 0x2000;
            this.buffers = new List<StringBuilder>();
            this.Initialize();
            this.Append(value);
        }

        public ChunkedStringBuilder Append(bool value) => 
            this.Append(value.ToString());

        public ChunkedStringBuilder Append(byte value) => 
            this.Append(value.ToString(CultureInfo.CurrentCulture));

        public IStringBuilder Append(char value)
        {
            StringBuilder item = this.buffers[this.buffers.Count - 1];
            if ((this.MaxBufferSize - item.Length) >= 1)
            {
                item.Append(value);
            }
            else
            {
                item = new StringBuilder(this.MaxBufferSize);
                item.Append(value);
                this.buffers.Add(item);
            }
            this.totalLength++;
            return this;
        }

        public ChunkedStringBuilder Append(decimal value) => 
            this.Append(value.ToString(CultureInfo.CurrentCulture));

        public ChunkedStringBuilder Append(double value) => 
            this.Append(value.ToString(CultureInfo.CurrentCulture));

        public ChunkedStringBuilder Append(short value) => 
            this.Append(value.ToString(CultureInfo.CurrentCulture));

        public ChunkedStringBuilder Append(int value) => 
            this.Append(value.ToString(CultureInfo.CurrentCulture));

        public ChunkedStringBuilder Append(long value) => 
            this.Append(value.ToString(CultureInfo.CurrentCulture));

        public ChunkedStringBuilder Append(object value) => 
            (value != null) ? this.Append(value.ToString()) : this;

        public ChunkedStringBuilder Append(float value) => 
            this.Append(value.ToString(CultureInfo.CurrentCulture));

        public ChunkedStringBuilder Append(string value) => 
            !string.IsNullOrEmpty(value) ? this.Append(value, 0, value.Length) : this;

        private ChunkedStringBuilder Append(IStringValueAdapter value, int startIndex, int count)
        {
            if (count > 0)
            {
                StringBuilder stringBuilder = this.buffers[this.buffers.Count - 1];
                int num = this.MaxBufferSize - stringBuilder.Length;
                if (num >= count)
                {
                    value.AppendToStringBuilder(stringBuilder, startIndex, count);
                }
                else
                {
                    int num2 = startIndex;
                    value.AppendToStringBuilder(stringBuilder, startIndex, num);
                    num2 += num;
                    int num3 = startIndex + count;
                    while (num2 < num3)
                    {
                        int num4 = Math.Min(num3 - num2, this.MaxBufferSize);
                        stringBuilder = new StringBuilder(this.MaxBufferSize);
                        value.AppendToStringBuilder(stringBuilder, num2, num4);
                        this.buffers.Add(stringBuilder);
                        num2 += num4;
                    }
                }
                this.totalLength += count;
            }
            return this;
        }

        public ChunkedStringBuilder Append(string value, int startIndex, int count) => 
            this.Append(new StringValueAdapter(value), startIndex, count);

        public ChunkedStringBuilder Append(char[] value, int startIndex, int count) => 
            this.Append(new CharArrayValueAdapter(value), startIndex, count);

        public void AppendExistingBuffersUnsafe(ChunkedStringBuilder stringBuilder)
        {
            if ((stringBuilder != null) && (stringBuilder.Length > 0))
            {
                if (this.MaxBufferSize != stringBuilder.MaxBufferSize)
                {
                    Exceptions.ThrowArgumentException("stringBuilder.MaxBufferSize", stringBuilder.MaxBufferSize);
                }
                this.Buffers.AddRange(stringBuilder.Buffers);
                this.totalLength += stringBuilder.totalLength;
                stringBuilder.Buffers.Clear();
                stringBuilder.Initialize();
            }
        }

        public ChunkedStringBuilder AppendLine() => 
            this.Append(Environment.NewLine);

        public ChunkedStringBuilder AppendLine(string value)
        {
            this.Append(value);
            return this.Append(Environment.NewLine);
        }

        public void Clear()
        {
            this.Initialize();
        }

        protected internal void Initialize()
        {
            int count = this.buffers.Count;
            if (count <= 0)
            {
                this.buffers.Add(new StringBuilder());
            }
            else
            {
                if (count > 1)
                {
                    this.buffers.RemoveRange(1, count - 1);
                }
                this.buffers[0].Length = 0;
            }
            this.totalLength = 0;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(this.Length);
            int count = this.buffers.Count;
            for (int i = 0; i < count; i++)
            {
                builder.Append(this.buffers[i].ToString());
            }
            return builder.ToString();
        }

        public string ToString(int startIndex, int length)
        {
            int num = startIndex / this.MaxBufferSize;
            int num2 = startIndex % this.MaxBufferSize;
            int num3 = ((startIndex + length) - 1) / this.MaxBufferSize;
            if (num == num3)
            {
                return this.buffers[num].ToString(num2, length);
            }
            StringBuilder builder = new StringBuilder(length);
            builder.Append(this.buffers[num].ToString(num2, this.MaxBufferSize - num2));
            for (int i = num + 1; i < num3; i++)
            {
                builder.Append(this.buffers[i].ToString());
            }
            int num4 = startIndex + length;
            builder.Append(this.buffers[num3].ToString(0, num4 - (num3 * this.MaxBufferSize)));
            return builder.ToString();
        }

        protected internal int MaxBufferSize
        {
            get => 
                this.maxBufferSize;
            set => 
                this.maxBufferSize = value;
        }

        protected internal List<StringBuilder> Buffers =>
            this.buffers;

        public int Length =>
            this.totalLength;

        public char this[int index]
        {
            get
            {
                int num = index / this.MaxBufferSize;
                int num2 = index % this.MaxBufferSize;
                return this.buffers[num][num2];
            }
            set
            {
                int num = index / this.MaxBufferSize;
                int num2 = index % this.MaxBufferSize;
                this.buffers[num][num2] = value;
            }
        }

        private class CharArrayValueAdapter : ChunkedStringBuilder.IStringValueAdapter
        {
            private readonly char[] value;

            internal CharArrayValueAdapter(char[] value)
            {
                this.value = value;
            }

            public void AppendToStringBuilder(StringBuilder stringBuilder, int startIndex, int count)
            {
                stringBuilder.Append(this.value, startIndex, count);
            }

            public int Length =>
                this.value.Length;
        }

        private interface IStringValueAdapter
        {
            void AppendToStringBuilder(StringBuilder stringBuilder, int startIndex, int count);

            int Length { get; }
        }

        private class StringValueAdapter : ChunkedStringBuilder.IStringValueAdapter
        {
            private readonly string value;

            internal StringValueAdapter(string value)
            {
                this.value = value;
            }

            public void AppendToStringBuilder(StringBuilder stringBuilder, int startIndex, int count)
            {
                stringBuilder.Append(this.value, startIndex, count);
            }

            public int Length =>
                this.value.Length;
        }
    }
}

