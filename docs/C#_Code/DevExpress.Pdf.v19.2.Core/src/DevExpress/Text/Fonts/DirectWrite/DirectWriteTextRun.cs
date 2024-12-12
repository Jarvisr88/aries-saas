namespace DevExpress.Text.Fonts.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.Text.Fonts;
    using System;
    using System.Runtime.CompilerServices;

    public class DirectWriteTextRun : IDXTextRun<DirectWriteTextRun>, IDXTextRun
    {
        public DirectWriteTextRun(string text, int offset, int length)
        {
            this.Text = new StringView(text, offset, length);
            this.<Offset>k__BackingField = offset;
            this.Length = length;
        }

        private DirectWriteTextRun(int offset, int length, StringView text, byte bidiLevel, DWRITE_SCRIPT_ANALYSIS script, DWRITE_LINE_BREAKPOINT[] breakpoints)
        {
            this.Text = text;
            this.<Offset>k__BackingField = offset;
            this.Length = length;
            this.BidiLevel = bidiLevel;
            this.Script = script;
            this.Breakpoints = breakpoints;
        }

        public DXLineBreakpoint GetBreakpoint(int index) => 
            new DXLineBreakpoint(this.Breakpoints[index + this.Offset]);

        public DirectWriteTextRun Split(int splitOffset)
        {
            int length = this.Length;
            this.Length = splitOffset - this.Offset;
            StringView text = this.Text;
            this.Text = text.SubView(0, this.Length);
            return new DirectWriteTextRun(splitOffset, length - this.Length, text.SubView(splitOffset - this.Offset), this.BidiLevel, this.Script, this.Breakpoints);
        }

        public int Offset { get; }

        public int Length { get; private set; }

        public byte BidiLevel { get; set; }

        public DWRITE_SCRIPT_ANALYSIS Script { get; set; }

        public DWRITE_LINE_BREAKPOINT[] Breakpoints { get; set; }

        public StringView Text { get; private set; }
    }
}

