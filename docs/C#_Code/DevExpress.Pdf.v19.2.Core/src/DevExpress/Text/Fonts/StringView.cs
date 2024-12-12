namespace DevExpress.Text.Fonts
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class StringView
    {
        private readonly string source;

        public StringView(string source, int offset, int length)
        {
            this.source = source;
            this.<Offset>k__BackingField = offset;
            this.<Length>k__BackingField = length;
        }

        public string GetText() => 
            this.source.Substring(this.Offset, this.Length);

        public StringView SubView(int splitOffset) => 
            this.SubView(splitOffset, this.Length - splitOffset);

        public StringView SubView(int splitOffset, int length) => 
            new StringView(this.source, this.Offset + splitOffset, length);

        public int Offset { get; }

        public int Length { get; }

        public char this[int index] =>
            this.source[this.Offset + index];
    }
}

