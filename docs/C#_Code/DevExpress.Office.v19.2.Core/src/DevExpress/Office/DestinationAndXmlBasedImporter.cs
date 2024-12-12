namespace DevExpress.Office
{
    using DevExpress.Office.Import.OpenXml;
    using DevExpress.Office.Localization;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Xml;

    public abstract class DestinationAndXmlBasedImporter : DestinationBasedImporter
    {
        private Stream baseStream;
        private IDocumentModel actualDocumentModel;
        private UnitsConverter unitsConverter;
        private readonly Dictionary<string, UriBasedOfficeImage> uniqueUriBasedImages;

        protected DestinationAndXmlBasedImporter(IDocumentModel documentModel) : base(documentModel)
        {
            this.uniqueUriBasedImages = new Dictionary<string, UriBasedOfficeImage>();
            this.actualDocumentModel = base.DocumentModel;
            this.unitsConverter = new UnitsConverter(base.DocumentModel.UnitConverter);
        }

        protected internal virtual void AfterImportMainDocument()
        {
        }

        protected internal virtual void BeforeImportMainDocument()
        {
        }

        public abstract void BeginSetMainDocumentContent();
        public abstract bool ConvertToBool(string value);
        protected internal abstract Destination CreateMainDocumentDestination();
        private UriBasedOfficeImage CreateUriBasedImageCore(string uri, int pixelTargetWidth, int pixelTargetHeight)
        {
            UriBasedOfficeImage image = new UriBasedOfficeImage(uri, pixelTargetWidth, pixelTargetHeight, base.DocumentModel, false, false);
            this.uniqueUriBasedImages.Add(uri, image);
            return image;
        }

        public virtual XmlReader CreateXmlReader(Stream stream) => 
            XmlReader.Create(stream, this.CreateXmlReaderSettings());

        protected internal virtual XmlReaderSettings CreateXmlReaderSettings() => 
            new XmlReaderSettings { 
                IgnoreComments = true,
                IgnoreWhitespace = true
            };

        public virtual string DecodeXmlChars(string val) => 
            XmlCharsDecoder.Decode(val);

        public abstract void EndSetMainDocumentContent();
        public int GetEighthPointsIntegerValue(XmlReader reader, string attributeName) => 
            this.GetWpSTIntegerValue(reader, attributeName, -2147483648);

        public int GetEighthPointsIntegerValue(XmlReader reader, string attributeName, int defaultValue) => 
            this.GetWpSTIntegerValue(reader, attributeName, NumberStyles.Integer, defaultValue);

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

        public int GetHalfPointsIntegerValue(XmlReader reader, string attributeName) => 
            this.GetWpSTIntegerValue(reader, attributeName, -2147483648);

        public int GetHalfPointsIntegerValue(XmlReader reader, string attributeName, int defaultValue) => 
            this.GetWpSTIntegerValue(reader, attributeName, NumberStyles.Integer, defaultValue);

        public int? GetIntegerNullableValue(XmlReader reader, string attr)
        {
            string str = this.ReadAttribute(reader, attr);
            if (!string.IsNullOrEmpty(str))
            {
                return new int?(this.GetIntegerValue(str, NumberStyles.Integer, -2147483648));
            }
            return null;
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
                if (this.AllowUniversalMeasureValues)
                {
                    ValueInfo info = StringValueParser.TryParse(value);
                    return ((info != null) ? ((int) Math.Round((double) this.unitsConverter.ValueUnitToModelUnitsF(info))) : defaultValue);
                }
                if (this.AllowIntPercentage && value.EndsWith("%"))
                {
                    value = value.Remove(value.Length - 1);
                    if (int.TryParse(value, numberStyles, CultureInfo.InvariantCulture, out num))
                    {
                        return (num * 0x3e8);
                    }
                }
                if (!this.IgnoreParseErrors)
                {
                    this.ThrowInvalidFile();
                }
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

        public long GetLongValue(XmlReader reader, string attributeName, long defaultValue)
        {
            string attribute = reader.GetAttribute(attributeName);
            return this.GetLongValue(attribute, defaultValue);
        }

        public bool? GetOnOffNullValue(XmlReader reader, string attributeName)
        {
            if (!string.IsNullOrEmpty(reader.GetAttribute(attributeName)))
            {
                return new bool?(this.GetWpSTOnOffValue(reader, attributeName));
            }
            return null;
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

        public int GetPointsIntegerValue(XmlReader reader, string attributeName) => 
            this.GetWpSTIntegerValue(reader, attributeName, -2147483648);

        public int GetPointsIntegerValue(XmlReader reader, string attributeName, int defaultValue) => 
            this.GetWpSTIntegerValue(reader, attributeName, NumberStyles.Integer, defaultValue);

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

        public bool? GetWpSTOnOffNullValue(XmlReader reader, string attributeName)
        {
            if (!string.IsNullOrEmpty(this.ReadAttribute(reader, attributeName)))
            {
                return new bool?(this.GetWpSTOnOffValue(reader, attributeName));
            }
            return null;
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
            byte num = 0;
            while (true)
            {
                try
                {
                    reader.Read();
                }
                catch
                {
                    if (this.ThrowExceptionOnReaderError)
                    {
                        throw;
                    }
                }
                if ((reader.ReadState == ReadState.EndOfFile) || (reader.ReadState == ReadState.Error))
                {
                    return;
                }
                if ((this.BaseStream != null) && (num == 0))
                {
                    base.ProgressIndication.SetProgress((int) this.BaseStream.Position);
                }
                this.ProcessCurrentDestination(reader);
                num = (byte) (num + 1);
            }
        }

        public virtual void ImportContent(XmlReader reader, Destination initialDestination)
        {
            int count = base.DestinationStack.Count;
            base.DestinationStack.Push(initialDestination);
            this.ImportContent(reader);
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
            if (baseStream != null)
            {
                base.ProgressIndication.Begin(OfficeLocalizer.GetString(OfficeStringId.Msg_Loading), (int) baseStream.Position, (int) (baseStream.Length - baseStream.Position), (int) baseStream.Position);
            }
            else
            {
                base.ProgressIndication.Begin(OfficeLocalizer.GetString(OfficeStringId.Msg_Loading), 0, 1, 0);
            }
            try
            {
                this.BeginSetMainDocumentContent();
                try
                {
                    this.ImportMainDocument(reader);
                }
                catch
                {
                    if (this.CreateEmptyDocumentOnLoadError)
                    {
                        this.SetMainDocumentEmptyContent();
                    }
                    throw;
                }
                finally
                {
                    this.EndSetMainDocumentContent();
                }
            }
            finally
            {
                base.ProgressIndication.End();
                this.baseStream = null;
            }
        }

        protected virtual void ImportThemeCore(XmlReader reader)
        {
            this.PrepareOfficeTheme();
            Destination initialDestination = new OfficeThemeDestination(this);
            initialDestination.ProcessElementOpen(reader);
            this.ImportContent(reader, initialDestination);
            this.ActualDocumentModel = base.DocumentModel;
        }

        protected internal virtual OfficeImage LookupExternalImageByRelation(OpenXmlRelation relation, string rootFolder)
        {
            UriBasedOfficeImage image;
            return (!this.uniqueUriBasedImages.TryGetValue(relation.Target, out image) ? ((OfficeImage) this.CreateUriBasedImageCore(relation.Target, 0, 0)) : ((OfficeImage) new UriBasedOfficeReferenceImage(image, 0, 0)));
        }

        public virtual UriBasedOfficeImageBase LookupExternalImageByRelationId(string relationId, string rootFolder)
        {
            UriBasedOfficeImage image;
            OpenXmlRelation relationById = this.DocumentRelations.LookupRelationById(relationId);
            return ((StringExtensions.CompareInvariantCultureIgnoreCase(relationById.TargetMode, "external") == 0) ? (!this.uniqueUriBasedImages.TryGetValue(relationById.Target, out image) ? ((UriBasedOfficeImageBase) this.CreateUriBasedImageCore(relationById.Target, 0, 0)) : ((UriBasedOfficeImageBase) new UriBasedOfficeReferenceImage(image, 0, 0))) : null);
        }

        protected internal OpenXmlRelation LookupExternalRelationById(string relationId)
        {
            OpenXmlRelation relationById = this.DocumentRelations.LookupRelationById(relationId);
            return ((StringExtensions.CompareInvariantCultureIgnoreCase(relationById.TargetMode, "external") == 0) ? relationById : null);
        }

        public virtual OfficeImage LookupImageByRelationId(IDocumentModel documentModel, string relationId, string rootFolder) => 
            null;

        protected abstract void PrepareOfficeTheme();
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

        public abstract void SetMainDocumentEmptyContent();

        protected internal Stream BaseStream
        {
            get => 
                this.baseStream;
            set => 
                this.baseStream = value;
        }

        public IDocumentModel ActualDocumentModel
        {
            get => 
                this.actualDocumentModel;
            set => 
                this.actualDocumentModel = value;
        }

        protected virtual bool CreateEmptyDocumentOnLoadError =>
            true;

        public abstract string RelationsNamespace { get; }

        public abstract string DocumentRootFolder { get; set; }

        public abstract OpenXmlRelationCollection DocumentRelations { get; }

        protected virtual bool ThrowExceptionOnReaderError =>
            false;

        public virtual bool AllowIntPercentage =>
            false;

        public virtual bool AllowUniversalMeasureValues =>
            false;
    }
}

