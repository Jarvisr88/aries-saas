namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Xml;

    public abstract class CompactXmlSerializer : XmlXtraSerializer
    {
        protected CompactXmlSerializer()
        {
        }

        protected XtraPropertyInfo CreateXmlPropertyInfo(string name, Type propertyType, object val, bool isKey)
        {
            XtraPropertyInfo info = this.CreateXtraPropertyInfo(name, propertyType, isKey, null);
            info.Value = val;
            return info;
        }

        protected virtual string GetObjectTypeName(object obj) => 
            obj.GetType().FullName;

        private bool IsValidName(string name)
        {
            try
            {
                XmlConvert.VerifyName(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected abstract string ObjToString(object val);
        protected void ReadAttributes(XmlReader reader, XtraPropertyInfo info, bool skipZeroDepth)
        {
            if (!skipZeroDepth || (reader.Depth != 0))
            {
                while (reader.MoveToNextAttribute())
                {
                    Type propertyType = null;
                    if (reader.Name.EndsWith("_type"))
                    {
                        propertyType = Type.GetType(reader.Value, false);
                        if (propertyType == null)
                        {
                            propertyType = base.ObjectConverterImpl.ResolveType(reader.Value);
                        }
                        reader.MoveToNextAttribute();
                    }
                    info.ChildProperties.Add(this.CreateXmlPropertyInfo(reader.Name, propertyType, reader.Value, false));
                }
            }
        }

        protected XtraPropertyInfo ReadInfoCore(XmlReader tr)
        {
            tr.Read();
            tr.MoveToContent();
            return this.ReadInfoCore(tr, true);
        }

        protected XtraPropertyInfo ReadInfoCore(XmlReader reader, bool skipZeroDepth)
        {
            if (reader.NodeType != XmlNodeType.Element)
            {
                return null;
            }
            XtraPropertyInfo info = this.CreateXmlPropertyInfo(reader.Name, null, null, true);
            bool isEmptyElement = reader.IsEmptyElement;
            this.ReadAttributes(reader, info, skipZeroDepth);
            if (isEmptyElement)
            {
                return info;
            }
            while (true)
            {
                if (reader.ReadState != System.Xml.ReadState.EndOfFile)
                {
                    reader.Read();
                    if (reader.NodeType != XmlNodeType.EndElement)
                    {
                        XtraPropertyInfo prop = this.ReadInfoCore(reader, false);
                        if (prop == null)
                        {
                            continue;
                        }
                        info.ChildProperties.Add(prop);
                        continue;
                    }
                }
                return info;
            }
        }

        protected void SerializeAttributeProperty(XmlWriter xmlWriter, XtraPropertyInfo pInfo)
        {
            object val = pInfo.Value;
            if ((val != null) && !val.GetType().IsPrimitive)
            {
                val = this.ObjToString(val);
            }
            if (this.IsValidName(pInfo.Name))
            {
                if (pInfo.Value != null)
                {
                    string str;
                    if (XtraPropertyInfo.ShouldSerializePropertyType(pInfo, out str, new Func<object, string>(this.GetObjectTypeName)))
                    {
                        xmlWriter.WriteAttributeString(pInfo.Name + "_type", str);
                    }
                    xmlWriter.WriteAttributeString(pInfo.Name, XmlObjectToString(val));
                }
                else if (this.WriteNullValues)
                {
                    xmlWriter.WriteAttributeString(pInfo.Name, null);
                }
            }
        }

        protected void SerializeContentProperty(XmlWriter tw, XtraPropertyInfo p)
        {
            if (p.ChildProperties.Count != 0)
            {
                this.SerializeContentPropertyCore(tw, p);
            }
        }

        protected virtual void SerializeContentPropertyCore(XmlWriter tw, XtraPropertyInfo p)
        {
            tw.WriteStartElement(p.Name.Replace("$", string.Empty));
            base.SerializeLevel(tw, p.ChildProperties);
            tw.WriteEndElement();
        }

        protected override void SerializeLevelCore(XmlWriter tw, IXtraPropertyCollection props)
        {
            if (!props.IsSinglePass)
            {
                foreach (XtraPropertyInfo info in props)
                {
                    if (!info.IsKey)
                    {
                        this.SerializeAttributeProperty(tw, info);
                    }
                }
            }
            foreach (XtraPropertyInfo info2 in props)
            {
                if (info2.IsKey)
                {
                    this.SerializeContentProperty(tw, info2);
                }
            }
        }

        protected override void WriteApplicationAttribute(string appName, XmlWriter tw)
        {
        }

        protected override void WriteStartDocument(XmlWriter tw)
        {
            tw.WriteStartDocument();
        }

        protected override string Version =>
            "19.2.9.0";

        protected virtual bool WriteNullValues =>
            false;
    }
}

