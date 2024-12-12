namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class QEncoding : QuotedPrintableEncoding
    {
        public static readonly QEncoding Instance = new QEncoding();

        protected internal override void AppendCharNoDecode(byte ch, List<byte> sb)
        {
            if (ch == 0x5f)
            {
                base.AppendCharNoDecode(0x20, sb);
            }
            else
            {
                base.AppendCharNoDecode(ch, sb);
            }
        }

        protected internal override void AppendCharNoDecode(char ch, StringBuilder sb)
        {
            if (ch == '_')
            {
                base.AppendCharNoDecode(' ', sb);
            }
            else
            {
                base.AppendCharNoDecode(ch, sb);
            }
        }
    }
}

