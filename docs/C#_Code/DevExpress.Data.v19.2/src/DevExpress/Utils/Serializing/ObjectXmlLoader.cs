namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Xml;

    public abstract class ObjectXmlLoader
    {
        private System.Xml.XmlNode root;
        private static Type intType = typeof(int);
        private static Type decimalType = typeof(decimal);
        private static Type dateTimeType = typeof(DateTime);
        private static Type stringType = typeof(string);
        private static Type booleanType = typeof(bool);
        private static Type objectType = typeof(object);

        protected ObjectXmlLoader(System.Xml.XmlNode root)
        {
            this.root = root;
        }

        protected internal virtual void FindRecursive(System.Xml.XmlNode root, string tagName, DXXmlNodeCollection searchResult)
        {
            DXXmlNodeCollection children = XmlDocumentHelper.GetChildren(root);
            int count = children.Count;
            for (int i = 0; i < count; i++)
            {
                System.Xml.XmlNode node = children[i];
                if (node.Name == tagName)
                {
                    searchResult.Add(node);
                }
                this.FindRecursive(node, tagName, searchResult);
            }
        }

        protected internal DXXmlNodeCollection GetChildNodes(string name) => 
            (this.root != null) ? this.GetElementsByTagName(this.root, name) : new DXXmlNodeCollection();

        protected internal DXXmlNodeCollection GetElementsByTagName(System.Xml.XmlNode root, string tagName)
        {
            DXXmlNodeCollection searchResult = new DXXmlNodeCollection();
            if (root.Name == tagName)
            {
                searchResult.Add(root);
            }
            this.FindRecursive(root, tagName, searchResult);
            return searchResult;
        }

        public abstract object ObjectFromXml();
        public bool ReadAttributeAsBoolean(string name, bool defaultValue)
        {
            object obj2 = this.ReadAttributeValueCore(name);
            return ((obj2 != null) ? Convert.ToBoolean(obj2) : defaultValue);
        }

        public Color ReadAttributeAsColor(string name, Color defaultValue)
        {
            object obj2 = this.ReadAttributeValueCore(name);
            return ((obj2 is string) ? ((Color) ObjectConverter.StringToObject((string) obj2, typeof(Color))) : defaultValue);
        }

        public DateTime ReadAttributeAsDateTime(string name, DateTime defaultValue)
        {
            object obj2 = this.ReadAttributeValueCore(name);
            return ((obj2 != null) ? DateTime.Parse(Convert.ToString(obj2), CultureInfo.InvariantCulture) : defaultValue);
        }

        public decimal ReadAttributeAsDecimal(string name, decimal defaultValue)
        {
            object obj2 = this.ReadAttributeValueCore(name);
            return ((obj2 is string) ? decimal.Parse((string) obj2) : defaultValue);
        }

        public Guid ReadAttributeAsGuid(string name, Guid defaultValue)
        {
            object obj2 = this.ReadAttributeValueCore(name);
            return ((obj2 is string) ? new Guid(Convert.ToString(obj2)) : defaultValue);
        }

        public Image ReadAttributeAsImage(string name, Image defaultValue)
        {
            object obj2 = this.ReadAttributeValueCore(name);
            return ((obj2 != null) ? (ObjectConverter.StringToObject(Convert.ToString(obj2), typeof(Image)) as Image) : defaultValue);
        }

        public int ReadAttributeAsInt(string name, int defaultValue)
        {
            object obj2 = this.ReadAttributeValueCore(name);
            return ((obj2 != null) ? Convert.ToInt32(obj2) : defaultValue);
        }

        public object ReadAttributeAsObject(string name, Type t, object defaultValue)
        {
            object obj2 = this.ReadAttributeValueCore(name);
            if (!(obj2 is string))
            {
                return defaultValue;
            }
            int result = -2147483648;
            return (int.TryParse((string) obj2, out result) ? result : ObjectConverter.StringToObject((string) obj2, t));
        }

        public string ReadAttributeAsString(string name, string defaultValue)
        {
            object obj2 = this.ReadAttributeValueCore(name);
            return ((obj2 != null) ? Convert.ToString(obj2) : defaultValue);
        }

        public TimeSpan ReadAttributeAsTimeSpan(string name, TimeSpan defaultValue)
        {
            object obj2 = this.ReadAttributeValueCore(name);
            return ((obj2 is string) ? TimeSpan.Parse((string) obj2) : defaultValue);
        }

        public object ReadAttributeValue(string name, Type t)
        {
            if (ReferenceEquals(t, intType))
            {
                return this.ReadAttributeAsInt(name, 0);
            }
            if (ReferenceEquals(t, decimalType))
            {
                return this.ReadAttributeAsDecimal(name, 0M);
            }
            if (ReferenceEquals(t, dateTimeType))
            {
                return this.ReadAttributeAsDateTime(name, DateTime.MinValue);
            }
            if (ReferenceEquals(t, stringType))
            {
                return this.ReadAttributeAsString(name, string.Empty);
            }
            if (ReferenceEquals(t, booleanType))
            {
                return this.ReadAttributeAsBoolean(name, false);
            }
            Type objectType = ObjectXmlLoader.objectType;
            Type type2 = t;
            return this.ReadAttributeAsObject(name, t, null);
        }

        protected internal object ReadAttributeValueCore(string name)
        {
            if (this.root == null)
            {
                return null;
            }
            System.Xml.XmlAttribute attribute = this.root.Attributes[name];
            return attribute?.Value;
        }
    }
}

