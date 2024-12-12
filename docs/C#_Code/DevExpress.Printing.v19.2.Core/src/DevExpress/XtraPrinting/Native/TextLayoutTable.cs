namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Reflection;

    public class TextLayoutTable : ITextLayoutTable
    {
        private string[] strings;

        public TextLayoutTable(string[] strings);

        public string this[int index] { get; set; }

        public int Count { get; }
    }
}

