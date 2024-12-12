namespace DevExpress.Utils.Text.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class StringCommand
    {
        private bool isStart = true;
        private string command = string.Empty;

        internal void SetEnd()
        {
            this.isStart = false;
        }

        internal void SetStart()
        {
            this.isStart = true;
        }

        public bool IsValid =>
            !string.IsNullOrEmpty(this.command);

        public string Command
        {
            get => 
                this.command;
            set => 
                this.command = value;
        }

        public bool IsStart =>
            this.isStart;

        public bool IsEnd =>
            !this.isStart;

        internal HtmlStringCommand Code { get; set; }

        public bool IsSeparator =>
            !string.IsNullOrEmpty(this.Command) && (this.Command[0].GetUnicodeCategory() == UnicodeCategory.OtherPunctuation);
    }
}

