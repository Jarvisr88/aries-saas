namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Xml;

    public abstract class DestinationXmImporter : DestinationImporter
    {
        private Stream baseStream;

        protected DestinationXmImporter()
        {
        }

        protected internal virtual void AfterImportMainDocument()
        {
        }

        protected internal virtual void BeforeImportMainDocument()
        {
        }

        public abstract bool ConvertToBool(string value);
        protected internal abstract Destination CreateMainDocumentDestination();
        public virtual XmlReader CreateXmlReader(Stream stream) => 
            XmlReader.Create(stream, this.CreateXmlReaderSettings());

        protected internal virtual XmlReaderSettings CreateXmlReaderSettings() => 
            new XmlReaderSettings { 
                IgnoreComments = true,
                IgnoreWhitespace = true
            };

        protected internal string DecodeXmlChars(string val) => 
            XmlCharsDecoder.Decode(val);

        public float GetFloatValue(string value, NumberStyles numberStyles, float defaultValue)
        {
            float num;
            return (string.IsNullOrEmpty(value) ? defaultValue : (!float.TryParse(value, numberStyles, CultureInfo.InvariantCulture, out num) ? -2.147484E+09f : num));
        }

        public float GetFloatValueInPoints(XmlReader reader, string attributeName, float defaultValue)
        {
            string attribute = reader.GetAttribute(attributeName, null);
            return (string.IsNullOrEmpty(attribute) ? float.MinValue : this.GetFloatValue(attribute.Replace("pt", ""), NumberStyles.Float, defaultValue));
        }

        public int GetIntegerValue(XmlReader reader, string attributeName) => 
            this.GetIntegerValue(reader, attributeName, -2147483648);

        public int GetIntegerValue(string value, NumberStyles numberStyles, int defaultValue)
        {
            if (!string.IsNullOrEmpty(value))
            {
                int num;
                long num2;
                double num3;
                if (int.TryParse(value, numberStyles, CultureInfo.InvariantCulture, out num))
                {
                    return num;
                }
                if (long.TryParse(value, numberStyles, CultureInfo.InvariantCulture, out num2))
                {
                    return (int) num2;
                }
                if (int.TryParse(value, numberStyles | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out num))
                {
                    return num;
                }
                if (double.TryParse(value, numberStyles | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out num3))
                {
                    return (int) num3;
                }
                if (this.IgnoreParseErrors)
                {
                    return defaultValue;
                }
                this.ThrowInvalidFile();
            }
            return defaultValue;
        }

        public int GetIntegerValue(XmlReader reader, string attributeName, int defaultValue) => 
            this.GetIntegerValue(reader, attributeName, NumberStyles.Integer, defaultValue);

        public int GetIntegerValue(XmlReader reader, string attributeName, NumberStyles numberStyles, int defaultValue)
        {
            string attribute = reader.GetAttribute(attributeName, null);
            return this.GetIntegerValue(attribute, numberStyles, defaultValue);
        }

        public int GetIntegerValueInPoints(XmlReader reader, string attributeName, int defaultValue)
        {
            string attribute = reader.GetAttribute(attributeName, null);
            return (string.IsNullOrEmpty(attribute) ? -2147483648 : this.GetIntegerValue(attribute.Replace("pt", ""), NumberStyles.Integer, defaultValue));
        }

        public long GetLongValue(string value, long defaultValue)
        {
            long num;
            return ((string.IsNullOrEmpty(value) || !long.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out num)) ? defaultValue : num);
        }

        public long GetLongValue(XmlReader reader, string attributeName)
        {
            string attribute = reader.GetAttribute(attributeName);
            return this.GetLongValue(attribute, -9223372036854775808L);
        }

        public bool GetOnOffValue(string value, bool defaultValue) => 
            string.IsNullOrEmpty(value) ? defaultValue : this.ConvertToBool(value);

        public bool GetOnOffValue(XmlReader reader, string attributeName) => 
            this.GetOnOffValue(reader, attributeName, true);

        public bool GetOnOffValue(XmlReader reader, string attributeName, bool defaultValue)
        {
            string attribute = reader.GetAttribute(attributeName, null);
            return this.GetOnOffValue(attribute, defaultValue);
        }

        public virtual double GetWpDoubleValue(XmlReader reader, string attributeName, double defaultValue)
        {
            string str = this.ReadAttribute(reader, attributeName);
            if (!string.IsNullOrEmpty(str))
            {
                double num;
                if (double.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
                {
                    return num;
                }
                this.ThrowInvalidFile();
            }
            return defaultValue;
        }

        public T? GetWpEnumOnOffNullValue<T>(XmlReader reader, string attributeName, Dictionary<T, string> table) where T: struct
        {
            string str = this.ReadAttribute(reader, attributeName);
            if (!string.IsNullOrEmpty(str))
            {
                return this.GetWpEnumOnOffNullValueCore<T>(str, table);
            }
            return null;
        }

        public T? GetWpEnumOnOffNullValueCore<T>(string value, Dictionary<T, string> table) where T: struct
        {
            T? nullable;
            using (Dictionary<T, string>.KeyCollection.Enumerator enumerator = table.Keys.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        T current = enumerator.Current;
                        string str = table[current];
                        if (value != str)
                        {
                            continue;
                        }
                        nullable = new T?(current);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return nullable;
        }

        public T GetWpEnumValue<T>(XmlReader reader, string attributeName, Dictionary<T, string> table, T defaultValue) where T: struct
        {
            string str = this.ReadAttribute(reader, attributeName);
            return (!string.IsNullOrEmpty(str) ? this.GetWpEnumValueCore<T>(str, table, defaultValue) : defaultValue);
        }

        public T GetWpEnumValue<T>(XmlReader reader, string attributeName, Dictionary<string, T> table, T defaultValue) where T: struct
        {
            string str = this.ReadAttribute(reader, attributeName);
            return (!string.IsNullOrEmpty(str) ? this.GetWpEnumValueCore<T>(str, table, defaultValue) : defaultValue);
        }

        public T GetWpEnumValue<T>(XmlReader reader, string attributeName, Dictionary<T, string> table, T defaultValue, string ns) where T: struct
        {
            string str = this.ReadAttribute(reader, attributeName, ns);
            return (!string.IsNullOrEmpty(str) ? this.GetWpEnumValueCore<T>(str, table, defaultValue) : defaultValue);
        }

        public T GetWpEnumValueCore<T>(string value, Dictionary<T, string> table, T defaultValue) where T: struct
        {
            T local2;
            using (Dictionary<T, string>.KeyCollection.Enumerator enumerator = table.Keys.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        T current = enumerator.Current;
                        string str = table[current];
                        if (value != str)
                        {
                            continue;
                        }
                        local2 = current;
                    }
                    else
                    {
                        return defaultValue;
                    }
                    break;
                }
            }
            return local2;
        }

        public T GetWpEnumValueCore<T>(string value, Dictionary<string, T> table, T defaultValue) where T: struct
        {
            T local;
            return (table.TryGetValue(value, out local) ? local : defaultValue);
        }

        public float GetWpSTFloatValue(string value, NumberStyles numberStyles, float defaultValue)
        {
            if (!string.IsNullOrEmpty(value))
            {
                double num;
                if (double.TryParse(value, numberStyles, CultureInfo.InvariantCulture, out num))
                {
                    return (float) num;
                }
                this.ThrowInvalidFile();
            }
            return defaultValue;
        }

        public float GetWpSTFloatValue(XmlReader reader, string attributeName, NumberStyles numberStyles, float defaultValue)
        {
            string str = this.ReadAttribute(reader, attributeName);
            return this.GetWpSTFloatValue(str, numberStyles, defaultValue);
        }

        public float GetWpSTFloatValue(XmlReader reader, string attributeName, NumberStyles numberStyles, float defaultValue, string ns)
        {
            string str = this.ReadAttribute(reader, attributeName, ns);
            return this.GetWpSTFloatValue(str, numberStyles, defaultValue);
        }

        public int GetWpSTIntegerValue(XmlReader reader, string attributeName) => 
            this.GetWpSTIntegerValue(reader, attributeName, -2147483648);

        public int GetWpSTIntegerValue(XmlReader reader, string attributeName, int defaultValue) => 
            this.GetWpSTIntegerValue(reader, attributeName, NumberStyles.Integer, defaultValue);

        public int GetWpSTIntegerValue(XmlReader reader, string attributeName, NumberStyles numberStyles, int defaultValue)
        {
            string str = this.ReadAttribute(reader, attributeName);
            return this.GetIntegerValue(str, numberStyles, defaultValue);
        }

        public bool GetWpSTOnOffValue(XmlReader reader, string attributeName) => 
            this.GetWpSTOnOffValue(reader, attributeName, true);

        public bool GetWpSTOnOffValue(XmlReader reader, string attributeName, bool defaultValue)
        {
            string str = this.ReadAttribute(reader, attributeName);
            return this.GetOnOffValue(str, defaultValue);
        }

        private void ImportContent(XmlReader reader)
        {
            while (true)
            {
                try
                {
                    reader.Read();
                }
                catch
                {
                }
                if ((reader.ReadState == System.Xml.ReadState.EndOfFile) || (reader.ReadState == System.Xml.ReadState.Error))
                {
                    return;
                }
                this.ProcessCurrentDestination(reader);
            }
        }

        public virtual void ImportContent(XmlReader reader, Destination initialDestination)
        {
            this.ImportContent(reader, initialDestination, new XmlContentImporter(this.ImportContent));
        }

        public void ImportContent(XmlReader reader, Destination initialDestination, XmlContentImporter import)
        {
            int count = base.DestinationStack.Count;
            base.DestinationStack.Push(initialDestination);
            import(reader);
            while (base.DestinationStack.Count > count)
            {
                base.DestinationStack.Pop();
            }
        }

        protected internal virtual void ImportMainDocument(XmlReader reader)
        {
            this.BeforeImportMainDocument();
            base.DestinationStack = new Stack<Destination>();
            this.ImportContent(reader, this.CreateMainDocumentDestination());
            this.AfterImportMainDocument();
        }

        protected internal virtual void ImportMainDocument(XmlReader reader, Stream baseStream)
        {
            this.baseStream = baseStream;
            try
            {
                this.ImportMainDocument(reader);
            }
            finally
            {
                this.baseStream = null;
            }
        }

        protected internal virtual void ProcessCurrentDestination(XmlReader reader)
        {
            base.DestinationStack.Peek().Process(reader);
        }

        public abstract string ReadAttribute(XmlReader reader, string attributeName);
        public abstract string ReadAttribute(XmlReader reader, string attributeName, string ns);
        public virtual bool ReadToRootElement(XmlReader reader, string name)
        {
            try
            {
                return reader.ReadToFollowing(name);
            }
            catch
            {
                return false;
            }
        }

        protected internal virtual bool ReadToRootElement(XmlReader reader, string name, string ns)
        {
            try
            {
                return reader.ReadToFollowing(name, ns);
            }
            catch
            {
                return false;
            }
        }

        protected internal void ThrowInvalidFile()
        {
            this.ThrowInvalidFile("Invalid file");
        }

        protected internal void ThrowInvalidFile(string message)
        {
            throw new Exception(message);
        }

        protected internal Stream BaseStream =>
            this.baseStream;
    }
}

