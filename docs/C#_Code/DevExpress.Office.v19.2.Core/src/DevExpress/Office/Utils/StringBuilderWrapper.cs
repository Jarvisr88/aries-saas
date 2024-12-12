namespace DevExpress.Office.Utils
{
    using System;
    using System.Text;

    public class StringBuilderWrapper : IStringBuilder
    {
        private readonly StringBuilder sb = new StringBuilder();

        public IStringBuilder Append(char ch)
        {
            this.sb.Append(ch);
            return this;
        }

        public void Clear()
        {
            this.sb.Length = 0;
        }

        public override string ToString() => 
            this.sb.ToString();

        public int Length =>
            this.sb.Length;
    }
}

