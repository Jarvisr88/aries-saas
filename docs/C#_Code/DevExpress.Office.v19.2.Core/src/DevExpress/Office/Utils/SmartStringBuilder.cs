namespace DevExpress.Office.Utils
{
    using System;

    public class SmartStringBuilder : IStringBuilder
    {
        private StringBuilderWrapper stringBuilder = new StringBuilderWrapper();
        private ChunkedStringBuilder stringBuilderEx;
        private IStringBuilder currentStringBuilder;

        public SmartStringBuilder()
        {
            this.currentStringBuilder = this.stringBuilder;
        }

        public IStringBuilder Append(char ch)
        {
            this.ChangeActiveStringBuilder(1);
            this.currentStringBuilder.Append(ch);
            return this;
        }

        private void ChangeActiveStringBuilder(int deltaLength)
        {
            if (ReferenceEquals(this.currentStringBuilder, this.stringBuilder) && ((this.Length + deltaLength) > 0x2000))
            {
                this.stringBuilderEx = new ChunkedStringBuilder(this.stringBuilder.ToString());
                this.stringBuilder = null;
                this.currentStringBuilder = this.stringBuilderEx;
            }
        }

        public void Clear()
        {
            if (ReferenceEquals(this.currentStringBuilder, this.stringBuilder))
            {
                this.currentStringBuilder.Clear();
            }
            else
            {
                this.stringBuilder = new StringBuilderWrapper();
                this.currentStringBuilder = this.stringBuilder;
                this.stringBuilderEx = null;
            }
        }

        public override string ToString() => 
            this.currentStringBuilder.ToString();

        public int Length =>
            this.currentStringBuilder.Length;
    }
}

