namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class TextDecorationsConverter : IOneTypeObjectConverter
    {
        public static readonly IOneTypeObjectConverter Instance = new TextDecorationsConverter();
        private const string None = "None";
        private const string separator = ",";
        private TextDecorationInfo[] predefinedTextDecorations = new TextDecorationInfo[] { new TextDecorationInfo(TextDecorations.OverLine[0], "OverLine"), new TextDecorationInfo(TextDecorations.Baseline[0], "Baseline"), new TextDecorationInfo(TextDecorations.Underline[0], "Underline"), new TextDecorationInfo(TextDecorations.Strikethrough[0], "Strikethrough") };

        public object FromString(string str) => 
            TextDecorationCollectionConverter.ConvertFromString(str);

        private string SerializeToString(TextDecorationCollection collection)
        {
            int index = 0;
            int count = collection.Count;
            List<string> values = new List<string>();
            while ((index < this.predefinedTextDecorations.Length) && (count > 0))
            {
                TextDecorationInfo info = this.predefinedTextDecorations[index];
                if (collection.Contains(info.Decoration))
                {
                    values.Add(info.Name);
                    count--;
                }
                index++;
            }
            return ((values.Count > 0) ? string.Join(",", values) : "None");
        }

        public string ToString(object obj)
        {
            TextDecorationCollection collection = obj as TextDecorationCollection;
            return ((collection != null) ? this.SerializeToString(collection) : "None");
        }

        public System.Type Type =>
            typeof(TextDecorationCollection);

        private class TextDecorationInfo
        {
            public TextDecorationInfo(TextDecoration decoration, string name)
            {
                this.Decoration = decoration;
                this.Name = name;
            }

            public TextDecoration Decoration { get; private set; }

            public string Name { get; private set; }
        }
    }
}

