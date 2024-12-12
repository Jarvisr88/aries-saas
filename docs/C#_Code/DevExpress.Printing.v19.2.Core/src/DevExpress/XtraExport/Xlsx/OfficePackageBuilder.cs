namespace DevExpress.XtraExport.Xlsx
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using DevExpress.Utils.Zip;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class OfficePackageBuilder
    {
        public const string RelsPrefix = "r";
        public const string RelsContentType = "application/vnd.openxmlformats-package.relationships+xml";
        public const string XmlContentType = "application/xml";
        public const string ContentTypesNamespaceConst = "http://schemas.openxmlformats.org/package/2006/content-types";
        public const string RelsNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        public const string OfficeDocumentNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
        public const string PackageRelsNamespace = "http://schemas.openxmlformats.org/package/2006/relationships";
        public const string RelsSharedStringsNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/sharedStrings";
        public const string RelsStylesNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles";
        private readonly Stream outputStream;
        private InternalZipArchive documentPackage;
        private DateTime now;
        private OpenXmlRelationCollection workbookRelations;
        private Dictionary<string, string> usedContentTypes;
        private Dictionary<string, string> overriddenContentTypes;

        protected OfficePackageBuilder()
        {
        }

        public OfficePackageBuilder(Stream outputStream)
        {
            Guard.ArgumentNotNull(outputStream, "outputStream");
            this.outputStream = outputStream;
        }

        public void BeginExport()
        {
            this.now = DateTimeHelper.Now;
            this.documentPackage = this.CreateDocumentPackage(this.outputStream);
            this.InitializeExport();
        }

        protected virtual InternalZipArchive CreateDocumentPackage(Stream stream) => 
            (stream != null) ? new InternalZipArchive(stream) : null;

        public void EndExport()
        {
            if (this.documentPackage != null)
            {
                ((IDisposable) this.documentPackage).Dispose();
            }
        }

        public void GenerateContentTypesContent(XmlWriter writer)
        {
            writer.WriteStartElement("Types", "http://schemas.openxmlformats.org/package/2006/content-types");
            try
            {
                this.GenerateUsedContentTypes(writer);
                this.GenerateOverriddenContentTypes(writer);
            }
            finally
            {
                writer.WriteEndElement();
            }
        }

        protected internal virtual void GenerateOverriddenContentTypes(XmlWriter writer)
        {
            foreach (string str in this.OverriddenContentTypes.Keys)
            {
                writer.WriteStartElement("Override");
                try
                {
                    writer.WriteAttributeString("PartName", str);
                    writer.WriteAttributeString("ContentType", this.OverriddenContentTypes[str]);
                }
                finally
                {
                    writer.WriteEndElement();
                }
            }
        }

        public virtual void GenerateRelationsContent(XmlWriter writer, OpenXmlRelation relation)
        {
            writer.WriteStartElement("Relationships", "http://schemas.openxmlformats.org/package/2006/relationships");
            try
            {
                this.WriteRelationshipTag(writer, relation);
            }
            finally
            {
                writer.WriteEndElement();
            }
        }

        public virtual void GenerateRelationsContent(XmlWriter writer, OpenXmlRelationCollection relations)
        {
            writer.WriteStartElement("Relationships", "http://schemas.openxmlformats.org/package/2006/relationships");
            try
            {
                this.GenerateRelationsContentCore(writer, relations);
            }
            finally
            {
                writer.WriteEndElement();
            }
        }

        protected internal void GenerateRelationsContentCore(XmlWriter writer, OpenXmlRelationCollection relations)
        {
            foreach (OpenXmlRelation relation in relations)
            {
                this.WriteRelationshipTag(writer, relation);
            }
        }

        protected internal virtual void GenerateUsedContentTypes(XmlWriter writer)
        {
            foreach (string str in this.UsedContentTypes.Keys)
            {
                string str2 = this.UsedContentTypes[str];
                writer.WriteStartElement("Default");
                try
                {
                    writer.WriteAttributeString("Extension", str);
                    writer.WriteAttributeString("ContentType", str2);
                }
                finally
                {
                    writer.WriteEndElement();
                }
            }
        }

        private void InitializeExport()
        {
            this.workbookRelations = new OpenXmlRelationCollection();
            this.usedContentTypes = new Dictionary<string, string>();
            this.overriddenContentTypes = new Dictionary<string, string>();
        }

        public void SetPackage(Stream stream)
        {
            this.documentPackage = this.CreateDocumentPackage(stream);
        }

        private void WriteRelationshipTag(XmlWriter writer, OpenXmlRelation relation)
        {
            writer.WriteStartElement("Relationship");
            try
            {
                writer.WriteAttributeString("Id", relation.Id);
                writer.WriteAttributeString("Type", relation.Type);
                string str = (StringExtensions.CompareInvariantCultureIgnoreCase(relation.TargetMode, "External") == 0) ? relation.Target.Replace(" ", "%20") : relation.Target;
                writer.WriteAttributeString("Target", str);
                if (!string.IsNullOrEmpty(relation.TargetMode))
                {
                    writer.WriteAttributeString("TargetMode", relation.TargetMode);
                }
            }
            finally
            {
                writer.WriteEndElement();
            }
        }

        public InternalZipArchive Package =>
            this.documentPackage;

        public DateTime Now =>
            this.now;

        public OpenXmlRelationCollection WorkbookRelations =>
            this.workbookRelations;

        public Dictionary<string, string> UsedContentTypes =>
            this.usedContentTypes;

        public Dictionary<string, string> OverriddenContentTypes =>
            this.overriddenContentTypes;
    }
}

