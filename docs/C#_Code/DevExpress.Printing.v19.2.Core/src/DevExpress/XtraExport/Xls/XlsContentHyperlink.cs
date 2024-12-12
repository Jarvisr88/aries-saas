namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Binary;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class XlsContentHyperlink : XlsContentBase
    {
        private const int fixedPartSize = 0x18;
        private XlsRef8 range = new XlsRef8();
        private Guid classId = BinaryHyperlinkObject.CLSID_StdHyperlink;
        private BinaryHyperlinkObject hyperlink = new BinaryHyperlinkObject();

        public override int GetSize() => 
            0x18 + this.hyperlink.GetSize();

        public override void Read(XlReader reader, int size)
        {
            this.range = XlsRef8.FromStream(reader);
            this.classId = new Guid(reader.ReadBytes(0x10));
            if (this.classId != BinaryHyperlinkObject.CLSID_StdHyperlink)
            {
                throw new Exception("Invalid XLS file: Wrong hyperlink class id");
            }
            this.hyperlink = BinaryHyperlinkObject.FromStream(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            this.range.Write(writer);
            writer.Write(this.classId.ToByteArray());
            this.hyperlink.Write(writer);
        }

        public XlsRef8 Range
        {
            get => 
                this.range;
            set
            {
                value ??= new XlsRef8();
                this.range = value;
            }
        }

        public Guid ClassId
        {
            get => 
                this.classId;
            set
            {
                Guard.ArgumentNotNull(value, "ClassId");
                this.classId = value;
            }
        }

        public bool HasMoniker
        {
            get => 
                this.hyperlink.HasMoniker;
            set => 
                this.hyperlink.HasMoniker = value;
        }

        public bool IsAbsolute
        {
            get => 
                this.hyperlink.IsAbsolute;
            set => 
                this.hyperlink.IsAbsolute = value;
        }

        public bool SiteGaveDisplayName
        {
            get => 
                this.hyperlink.SiteGaveDisplayName;
            set => 
                this.hyperlink.SiteGaveDisplayName = value;
        }

        public bool HasLocationString
        {
            get => 
                this.hyperlink.HasLocationString;
            set => 
                this.hyperlink.HasLocationString = value;
        }

        public bool HasDisplayName
        {
            get => 
                this.hyperlink.HasDisplayName;
            set => 
                this.hyperlink.HasDisplayName = value;
        }

        public bool HasGUID
        {
            get => 
                this.hyperlink.HasGUID;
            set => 
                this.hyperlink.HasGUID = value;
        }

        public bool HasCreationTime
        {
            get => 
                this.hyperlink.HasCreationTime;
            set => 
                this.hyperlink.HasCreationTime = value;
        }

        public bool HasFrameName
        {
            get => 
                this.hyperlink.HasFrameName;
            set => 
                this.hyperlink.HasFrameName = value;
        }

        public bool IsMonkerSavedAsString
        {
            get => 
                this.hyperlink.IsMonkerSavedAsString;
            set => 
                this.hyperlink.IsMonkerSavedAsString = value;
        }

        public bool IsAbsoluteFromRelative
        {
            get => 
                this.hyperlink.IsAbsoluteFromRelative;
            set => 
                this.hyperlink.IsAbsoluteFromRelative = value;
        }

        public string DisplayName
        {
            get => 
                this.hyperlink.DisplayName;
            set => 
                this.hyperlink.DisplayName = value;
        }

        public string FrameName
        {
            get => 
                this.hyperlink.FrameName;
            set => 
                this.hyperlink.FrameName = value;
        }

        public string Moniker
        {
            get => 
                this.hyperlink.Moniker;
            set => 
                this.hyperlink.Moniker = value;
        }

        public BinaryHyperlinkMonikerBase OleMoniker
        {
            get => 
                this.hyperlink.OleMoniker;
            set => 
                this.hyperlink.OleMoniker = value;
        }

        public string Location
        {
            get => 
                this.hyperlink.Location;
            set => 
                this.hyperlink.Location = value;
        }

        public Guid OptionalGUID
        {
            get => 
                this.hyperlink.OptionalGUID;
            set => 
                this.hyperlink.OptionalGUID = value;
        }

        public long CreationTime
        {
            get => 
                this.hyperlink.CreationTime;
            set => 
                this.hyperlink.CreationTime = value;
        }
    }
}

