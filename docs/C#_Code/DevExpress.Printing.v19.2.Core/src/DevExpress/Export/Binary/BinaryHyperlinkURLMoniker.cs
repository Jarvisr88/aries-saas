namespace DevExpress.Export.Binary
{
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class BinaryHyperlinkURLMoniker : BinaryHyperlinkMonikerBase
    {
        private const uint maskAllowRelative = 1;
        private const uint maskAllowImplicitWildcardScheme = 2;
        private const uint maskAllowImplicitFileScheme = 4;
        private const uint maskNoFrag = 8;
        private const uint maskNoCanonicalize = 0x10;
        private const uint maskCanonicalize = 0x20;
        private const uint maskFileUseDosPath = 0x40;
        private const uint maskDecodeExtraInfo = 0x80;
        private const uint maskNoDecodeExtraInfo = 0x100;
        private const uint maskCrackUnknownSchemes = 0x200;
        private const uint maskNoCrackUnknownSchemes = 0x400;
        private const uint maskPreProcessHtmlUri = 0x800;
        private const uint maskNoPreProcessHtmlUri = 0x1000;
        private const uint maskIESettings = 0x2000;
        private const uint maskNoIESettings = 0x4000;
        private const uint maskNoEncodeForbiddenChars = 0x8000;
        private const uint maskAllFlags = 0xffff;
        private static readonly Guid serialGUID = new Guid("{0xF4815879, 0x1D3B, 0x487F, {0xAF, 0x2C, 0x82, 0x5D, 0xC4, 0x85, 0x27, 0x63}}");
        private NullTerminatedUnicodeString url;
        private uint uriCreateFlags;

        public BinaryHyperlinkURLMoniker() : base(BinaryHyperlinkMonikerFactory.CLSID_URLMoniker)
        {
            this.url = new NullTerminatedUnicodeString();
            this.uriCreateFlags = 0x5510;
        }

        public static BinaryHyperlinkURLMoniker FromStream(XlReader reader)
        {
            BinaryHyperlinkURLMoniker moniker = new BinaryHyperlinkURLMoniker();
            moniker.Read(reader);
            return moniker;
        }

        private bool GetFlag(uint mask) => 
            (this.uriCreateFlags & mask) != 0;

        public override int GetSize()
        {
            int length = this.url.Length;
            if (this.HasOptionalData)
            {
                length += 0x18;
            }
            return ((base.GetSize() + 4) + length);
        }

        protected void Read(XlReader reader)
        {
            int num = reader.ReadInt32();
            this.url = NullTerminatedUnicodeString.FromStream(reader);
            if (num > this.url.Length)
            {
                this.HasOptionalData = true;
                Guid guid = new Guid(reader.ReadBytes(0x10));
                if (guid != serialGUID)
                {
                    throw new Exception("Invalid binary file: Unknown serial GUID in hyperlink URL moniker");
                }
                reader.ReadInt32();
                this.uriCreateFlags = reader.ReadUInt32() & 0xffff;
            }
        }

        private void SetFlag(uint mask, bool value)
        {
            if (value)
            {
                this.uriCreateFlags |= mask;
            }
            else
            {
                this.uriCreateFlags &= ~mask;
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            int length = this.url.Length;
            if (this.HasOptionalData)
            {
                length += 0x18;
            }
            writer.Write(length);
            this.url.Write(writer);
            if (this.HasOptionalData)
            {
                writer.Write(serialGUID.ToByteArray());
                writer.Write(0);
                writer.Write(this.uriCreateFlags);
            }
        }

        public string Url
        {
            get => 
                this.url.Value;
            set => 
                this.url.Value = value;
        }

        public bool HasOptionalData { get; set; }

        public bool AllowRelative
        {
            get => 
                this.GetFlag(1);
            set => 
                this.SetFlag(1, value);
        }

        public bool AllowImplicitWildcardScheme
        {
            get => 
                this.GetFlag(2);
            set => 
                this.SetFlag(2, value);
        }

        public bool AllowImplicitFileScheme
        {
            get => 
                this.GetFlag(4);
            set => 
                this.SetFlag(4, value);
        }

        public bool NoFrag
        {
            get => 
                this.GetFlag(8);
            set => 
                this.SetFlag(8, value);
        }

        public bool Canonicalize
        {
            get => 
                this.GetFlag(0x20);
            set
            {
                this.SetFlag(0x10, !value);
                this.SetFlag(0x20, value);
            }
        }

        public bool FileUseDosPath
        {
            get => 
                this.GetFlag(0x40);
            set => 
                this.SetFlag(0x40, value);
        }

        public bool DecodeExtraInfo
        {
            get => 
                this.GetFlag(0x80);
            set
            {
                this.SetFlag(0x80, value);
                this.SetFlag(0x100, !value);
            }
        }

        public bool CrackUnknownSchemes
        {
            get => 
                this.GetFlag(0x200);
            set
            {
                this.SetFlag(0x200, value);
                this.SetFlag(0x400, !value);
            }
        }

        public bool PreProcessHtmlUri
        {
            get => 
                this.GetFlag(0x800);
            set
            {
                this.SetFlag(0x800, value);
                this.SetFlag(0x1000, !value);
            }
        }

        public bool IESettings
        {
            get => 
                this.GetFlag(0x2000);
            set
            {
                this.SetFlag(0x2000, value);
                this.SetFlag(0x4000, !value);
            }
        }

        public bool NoEncodeForbiddenChars
        {
            get => 
                this.GetFlag(0x8000);
            set => 
                this.SetFlag(0x8000, value);
        }
    }
}

