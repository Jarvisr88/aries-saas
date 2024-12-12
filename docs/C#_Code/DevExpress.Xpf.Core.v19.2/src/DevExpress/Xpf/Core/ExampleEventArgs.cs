namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExampleEventArgs : EventArgs
    {
        public ExampleEventArgs(string value)
        {
            this.Value = value;
        }

        public string Value { get; set; }
    }
}

