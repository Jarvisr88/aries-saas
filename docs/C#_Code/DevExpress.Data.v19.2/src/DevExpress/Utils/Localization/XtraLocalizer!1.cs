namespace DevExpress.Utils.Localization
{
    using DevExpress.Utils;
    using DevExpress.Utils.Localization.Internal;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Threading;
    using System.Xml;

    public abstract class XtraLocalizer<T> where T: struct
    {
        private static ActiveLocalizerProvider<T> localizerProvider;
        private ConcurrentDictionary<CultureInfo, ConcurrentDictionary<T, string>> stringTables;

        public static event EventHandler ActiveChanged
        {
            add
            {
                ActiveLocalizerChangedWeakEventHandler<T>.AddHandler(value);
            }
            remove
            {
                ActiveLocalizerChangedWeakEventHandler<T>.RemoveHandler(value);
            }
        }

        protected XtraLocalizer()
        {
            this.stringTables = new ConcurrentDictionary<CultureInfo, ConcurrentDictionary<T, string>>();
        }

        protected virtual void AddString(T id, string str)
        {
            this.StringTable[id] = str;
        }

        protected virtual IEqualityComparer<T> CreateComparer() => 
            EqualityComparer<T>.Default;

        public abstract XtraLocalizer<T> CreateResXLocalizer();
        protected internal virtual void CreateStringTable()
        {
            this.PopulateStringTable();
        }

        public virtual XmlDocument CreateXmlDocument()
        {
            string enumTypeName = this.GetEnumTypeName();
            XmlDocument document = SafeXml.CreateDocument(null);
            document.AppendChild(document.CreateXmlDeclaration("1.0", "utf-8", string.Empty));
            XmlElement newChild = document.CreateElement("root");
            document.AppendChild(newChild);
            T[] values = (T[]) Enum.GetValues(typeof(T));
            string[] names = Enum.GetNames(typeof(T));
            int length = values.Length;
            for (int i = 0; i < length; i++)
            {
                XmlElement element2 = document.CreateElement("data");
                newChild.AppendChild(element2);
                System.Xml.XmlAttribute node = document.CreateAttribute("name");
                node.Value = $"{enumTypeName}.{names[i]}";
                element2.Attributes.Append(node);
                XmlElement element3 = document.CreateElement("value");
                element2.AppendChild(element3);
                XmlText text = document.CreateTextNode("value");
                text.Value = this.GetLocalizedString(values[i]);
                element3.AppendChild(text);
            }
            return document;
        }

        protected virtual bool DiffersFromCurrentCulture() => 
            true;

        public static ActiveLocalizerProvider<T> GetActiveLocalizerProvider() => 
            XtraLocalizer<T>.localizerProvider;

        protected internal virtual string GetEnumTypeName() => 
            typeof(T).Name;

        public virtual string GetLocalizedString(T id)
        {
            string str;
            return (this.StringTable.TryGetValue(id, out str) ? str : string.Empty);
        }

        protected abstract void PopulateStringTable();
        public static void RaiseActiveChanged()
        {
            ActiveLocalizerChangedWeakEventHandler<T>.RaiseChanged(XtraLocalizer<T>.Active);
        }

        public static void SetActiveLocalizerProvider(ActiveLocalizerProvider<T> value)
        {
            XtraLocalizer<T>.localizerProvider = value;
        }

        public virtual void WriteToXml(string fileName)
        {
            XmlDocument document = this.CreateXmlDocument();
            XmlTextWriter w = new XmlTextWriter(fileName, Encoding.UTF8);
            try
            {
                w.Formatting = Formatting.Indented;
                document.WriteTo(w);
            }
            finally
            {
                w.Flush();
                w.Close();
            }
        }

        public virtual string Language =>
            "English";

        internal IDictionary<T, string> StringTable
        {
            get
            {
                ConcurrentDictionary<T, string> dictionary;
                CultureInfo currentUICulture = this.CurrentUICulture;
                if (!this.stringTables.TryGetValue(currentUICulture, out dictionary))
                {
                    dictionary = new ConcurrentDictionary<T, string>(this.CreateComparer());
                    this.stringTables[currentUICulture] = dictionary;
                    this.PopulateStringTable();
                }
                return dictionary;
            }
        }

        public static XtraLocalizer<T> Active
        {
            get => 
                XtraLocalizer<T>.localizerProvider.GetActiveLocalizer();
            set
            {
                XtraLocalizer<T> activeLocalizer = XtraLocalizer<T>.localizerProvider.GetActiveLocalizer();
                value ??= activeLocalizer.CreateResXLocalizer();
                if (!ReferenceEquals(activeLocalizer, value))
                {
                    XtraLocalizer<T>.localizerProvider.SetActiveLocalizer(value);
                    XtraLocalizer<T>.RaiseActiveChanged();
                }
            }
        }

        private CultureInfo CurrentUICulture =>
            Thread.CurrentThread.CurrentUICulture;
    }
}

