namespace DevExpress.Office.OpenXml.Export
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public abstract class OpenXmlExportWalkerBase
    {
        protected XmlWriter DocumentContentWriter;

        protected OpenXmlExportWalkerBase(XmlWriter documentContentWriter)
        {
            this.DocumentContentWriter = documentContentWriter;
        }

        protected internal virtual void WriteBoolValue(string tag, bool value)
        {
            OpenXmlDrawingExportHelper.WriteBoolValue(this.DocumentContentWriter, tag, value);
        }

        protected internal virtual void WriteEndElement()
        {
            OpenXmlDrawingExportHelper.WriteEndElement(this.DocumentContentWriter);
        }

        protected internal virtual void WriteEnumValue<T>(string attr, T value, Dictionary<T, string> table)
        {
            OpenXmlDrawingExportHelper.WriteEnumValue<T>(this.DocumentContentWriter, attr, value, table);
        }

        protected internal virtual void WriteEnumValue<T>(string attr, T value, Dictionary<T, string> table, T defaultValue)
        {
            OpenXmlDrawingExportHelper.WriteEnumValue<T>(this.DocumentContentWriter, attr, value, table, defaultValue);
        }

        protected internal virtual void WriteIntValue(string tag, int value)
        {
            OpenXmlDrawingExportHelper.WriteIntValue(this.DocumentContentWriter, tag, value);
        }

        protected internal virtual void WriteIntValue(string tag, int value, int defaultValue)
        {
            OpenXmlDrawingExportHelper.WriteIntValue(this.DocumentContentWriter, tag, value, defaultValue);
        }

        protected internal virtual void WriteLongValue(string tag, long value)
        {
            OpenXmlDrawingExportHelper.WriteLongValue(this.DocumentContentWriter, tag, value);
        }

        protected internal virtual void WriteLongValue(string tag, long value, int defaultValue)
        {
            OpenXmlDrawingExportHelper.WriteLongValue(this.DocumentContentWriter, tag, value, defaultValue);
        }

        protected internal virtual void WriteStartElement(string tag)
        {
            OpenXmlDrawingExportHelper.WriteStartElement(this.DocumentContentWriter, tag, "http://schemas.openxmlformats.org/drawingml/2006/main");
        }

        protected internal virtual void WriteStringValue(string tag, string value)
        {
            OpenXmlDrawingExportHelper.WriteStringValue(this.DocumentContentWriter, tag, value);
        }
    }
}

