namespace DevExpress.Text.Fonts
{
    using System;
    using System.Runtime.CompilerServices;

    public class DXTextRun
    {
        public DXTextRun(int startIndex, int endIndex, byte bidiLevel)
        {
            this.<StartIndex>k__BackingField = startIndex;
            this.<EndIndex>k__BackingField = endIndex;
            this.<BidiLevel>k__BackingField = bidiLevel;
        }

        public override bool Equals(object obj)
        {
            DXTextRun run = obj as DXTextRun;
            return ((run != null) && ((this.StartIndex == run.StartIndex) && ((this.EndIndex == run.EndIndex) && (this.BidiLevel == run.BidiLevel))));
        }

        public override int GetHashCode() => 
            (((((-1858012434 * -1521134295) + this.StartIndex.GetHashCode()) * -1521134295) + this.EndIndex.GetHashCode()) * -1521134295) + this.BidiLevel.GetHashCode();

        public override string ToString() => 
            $"[Start:{this.StartIndex} End:{this.EndIndex} Bidi:{this.BidiLevel}]";

        public int StartIndex { get; }

        public int EndIndex { get; }

        public byte BidiLevel { get; }
    }
}

