namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    internal abstract class RtfControl
    {
        protected StringBuilder builder = new StringBuilder();

        protected RtfControl()
        {
        }

        public virtual string Compile()
        {
            string str;
            try
            {
                str = this.builder.ToString();
            }
            finally
            {
                this.builder.Clear();
            }
            return str;
        }

        protected void WriteChild(RtfControl rtfControl, bool newLine = false)
        {
            this.WriteChild(this.builder, rtfControl, newLine);
        }

        protected void WriteChild(StringBuilder builder, RtfControl rtfControl, bool newLine = false)
        {
            if (newLine)
            {
                builder.AppendLine();
            }
            builder.Append(rtfControl.Compile());
        }

        protected void WriteCloseBrace()
        {
            this.WriteCloseBrace(this.builder);
        }

        protected void WriteCloseBrace(StringBuilder builder)
        {
            builder.Append("}");
        }

        protected void WriteKeyword(Keyword keyword, int? value = new int?(), bool semicolon = false, bool newLine = false)
        {
            this.WriteKeyword(this.builder, keyword, value, semicolon, newLine);
        }

        protected void WriteKeyword(StringBuilder builder, Keyword keyword, int? value = new int?(), bool semicolon = false, bool newLine = false)
        {
            if (newLine)
            {
                builder.AppendLine();
            }
            builder.Append((string) keyword);
            if (value != null)
            {
                builder.Append(value.Value);
            }
            if (semicolon)
            {
                builder.Append(";");
            }
        }

        protected void WriteOpenBrace()
        {
            this.WriteOpenBrace(this.builder);
        }

        protected void WriteOpenBrace(StringBuilder builder)
        {
            builder.Append("{");
        }

        protected void WriteRawCompiled(string compiled, bool newLine = false)
        {
            this.WriteRawCompiled(this.builder, compiled, newLine);
        }

        protected void WriteRawCompiled(StringBuilder builder, string compiled, bool newLine = false)
        {
            if (newLine)
            {
                builder.AppendLine();
            }
            builder.Append(compiled);
        }

        protected void WriteSpace()
        {
            this.WriteSpace(this.builder);
        }

        protected void WriteSpace(StringBuilder builder)
        {
            builder.Append(" ");
        }

        protected void WriteValue(string value, bool semicolon = false)
        {
            this.WriteValue(this.builder, value, semicolon);
        }

        protected void WriteValue(StringBuilder builder, string value, bool semicolon = false)
        {
            builder.Append(value);
            if (semicolon)
            {
                builder.Append(";");
            }
        }
    }
}

