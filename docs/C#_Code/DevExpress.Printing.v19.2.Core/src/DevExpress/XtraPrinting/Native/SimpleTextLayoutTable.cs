namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Reflection;

    public class SimpleTextLayoutTable : ITextLayoutTable
    {
        public static SimpleTextLayoutTable Empty;
        private string text;

        static SimpleTextLayoutTable();
        public SimpleTextLayoutTable(string text);

        public string this[int index] { get; set; }

        public int Count { get; }
    }
}

