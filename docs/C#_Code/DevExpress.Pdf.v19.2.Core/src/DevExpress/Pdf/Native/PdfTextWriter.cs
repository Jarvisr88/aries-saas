namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfTextWriter
    {
        private static byte[] showTextCommand = new byte[] { 0x20, 0x54, 0x6a };
        private static byte[] showTextWithGlyphPositioningCommand = new byte[] { 0x20, 0x54, 0x4a };
        private readonly PdfStreamWriter streamWriter;
        private readonly bool useTwoByteCodePoints;
        private List<byte> textBuffer = new List<byte>(0x20);
        private bool useGlyphOffsets;

        public PdfTextWriter(PdfStreamWriter streamWriter, bool useTwoByteCodePoints)
        {
            this.streamWriter = streamWriter;
            this.useTwoByteCodePoints = useTwoByteCodePoints;
        }

        internal void AppendGlyph(ushort index)
        {
            if (this.useTwoByteCodePoints)
            {
                this.textBuffer.Add((byte) (index >> 8));
            }
            this.textBuffer.Add((byte) index);
        }

        public void AppendOffset(float offset)
        {
            if (Math.Abs(offset) >= 0.0001)
            {
                if (!this.useGlyphOffsets)
                {
                    this.useGlyphOffsets = true;
                    this.streamWriter.WriteSpace();
                    this.streamWriter.WriteOpenBracket();
                }
                this.AppendTextBuffer();
                this.streamWriter.WriteDouble((double) offset, true);
            }
        }

        private void AppendTextBuffer()
        {
            if (this.textBuffer.Count > 0)
            {
                this.streamWriter.WriteSpace();
                this.streamWriter.WriteHexadecimalString(this.textBuffer);
                this.textBuffer.Clear();
            }
        }

        public void EndText()
        {
            if (this.useGlyphOffsets)
            {
                this.AppendTextBuffer();
                this.streamWriter.WriteClosedBracket();
                this.streamWriter.Write(showTextWithGlyphPositioningCommand);
            }
            else if (this.textBuffer.Count > 0)
            {
                this.AppendTextBuffer();
                this.streamWriter.Write(showTextCommand);
            }
            this.useGlyphOffsets = false;
        }
    }
}

