namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;
    using System.Text;

    public class PdfName : PdfObject
    {
        private const string escape = "%()<>[]{}/#";
        private string value;

        public PdfName(string value)
        {
            this.value = value;
        }

        private string EscapeName(string name)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < name.Length; i++)
            {
                if (!IsEscapeChar(name[i]) && ((name[i] >= '!') && (name[i] <= '~')))
                {
                    builder.Append(name[i]);
                }
                else
                {
                    builder.Append("#");
                    builder.Append(PdfStringUtils.HexCharAsByte(name[i]));
                }
            }
            return builder.ToString();
        }

        public static bool IsEscapeChar(char ch) => 
            "%()<>[]{}/#".IndexOf(ch) >= 0;

        protected override void WriteContent(StreamWriter writer)
        {
            writer.Write("/" + this.EscapeName(this.value));
        }

        public string Value =>
            this.value;
    }
}

