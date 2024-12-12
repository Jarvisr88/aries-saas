namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfSignatureByteRange : PdfArray
    {
        private long start;
        private long end;

        public void Patch(StreamWriter writer, long excludedRegionStart, long excludedRegionLength)
        {
            writer.Flush();
            int num = (int) (excludedRegionStart + excludedRegionLength);
            int num2 = ((int) writer.BaseStream.Length) - num;
            writer.BaseStream.Position = this.start;
            base.Clear();
            int[] numbers = new int[4];
            numbers[1] = (int) excludedRegionStart;
            numbers[2] = num;
            numbers[3] = num2;
            base.AddRange(numbers);
            base.WriteContent(writer);
            writer.Flush();
            for (long i = writer.BaseStream.Position; i < this.end; i += 1L)
            {
                writer.Write(' ');
            }
        }

        protected override void WriteContent(StreamWriter writer)
        {
            writer.Flush();
            this.start = writer.BaseStream.Position;
            int start = (int) this.start;
            int num2 = (int) this.start;
            int[] numbers = new int[4];
            numbers[1] = start;
            numbers[2] = num2;
            numbers[3] = 0x7fffffff;
            base.AddRange(numbers);
            base.WriteContent(writer);
            writer.Flush();
            this.end = writer.BaseStream.Position;
        }
    }
}

