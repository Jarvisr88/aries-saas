namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentDv : XlsContentBase
    {
        private static readonly char[] nullChar = new char[1];
        private const string nullString = "\0";
        private XLUnicodeString promptTitle;
        private XLUnicodeString errorTitle;
        private XLUnicodeString prompt;
        private XLUnicodeString error;
        private byte[] formula1Bytes;
        private byte[] formula2Bytes;
        private readonly List<XlsRef8> ranges;

        public XlsContentDv()
        {
            XLUnicodeString text1 = new XLUnicodeString();
            text1.Value = "\0";
            this.promptTitle = text1;
            XLUnicodeString text2 = new XLUnicodeString();
            text2.Value = "\0";
            this.errorTitle = text2;
            XLUnicodeString text3 = new XLUnicodeString();
            text3.Value = "\0";
            this.prompt = text3;
            XLUnicodeString text4 = new XLUnicodeString();
            text4.Value = "\0";
            this.error = text4;
            this.formula1Bytes = new byte[0];
            this.formula2Bytes = new byte[0];
            this.ranges = new List<XlsRef8>();
        }

        public override int GetSize() => 
            ((((((14 + this.promptTitle.Length) + this.errorTitle.Length) + this.prompt.Length) + this.error.Length) + this.formula1Bytes.Length) + this.formula2Bytes.Length) + (this.Ranges.Count * 8);

        public override void Read(XlReader reader, int size)
        {
            uint num = reader.ReadUInt32();
            this.ValidationType = ((XlDataValidationType) num) & ((XlDataValidationType) 15);
            this.ErrorStyle = (XlDataValidationErrorStyle) ((num & 0x70) >> 4);
            this.StringLookup = (num & 0x80) != 0;
            this.AllowBlank = (num & 0x100) != 0;
            this.SuppressCombo = (num & 0x200) != 0;
            this.ImeMode = (XlDataValidationImeMode) ((num & 0x3fc00) >> 10);
            this.ShowInputMessage = (num & 0x40000) != 0;
            this.ShowErrorMessage = (num & 0x80000) != 0;
            this.ValidationOperator = (XlDataValidationOperator) ((num & 0xf00000) >> 20);
            this.promptTitle = XLUnicodeString.FromStream(reader);
            this.errorTitle = XLUnicodeString.FromStream(reader);
            this.prompt = XLUnicodeString.FromStream(reader);
            this.error = XLUnicodeString.FromStream(reader);
            int count = reader.ReadUInt16();
            reader.ReadUInt16();
            this.formula1Bytes = (count <= 0) ? new byte[0] : reader.ReadBytes(count);
            count = reader.ReadUInt16();
            reader.ReadUInt16();
            this.formula2Bytes = (count <= 0) ? new byte[0] : reader.ReadBytes(count);
            int num3 = reader.ReadUInt16();
            for (int i = 0; i < num3; i++)
            {
                this.Ranges.Add(XlsRef8.FromStream(reader));
            }
        }

        public override void Write(BinaryWriter writer)
        {
            uint num = (uint) (this.ValidationType | (((int) this.ErrorStyle) << 4));
            if (this.StringLookup)
            {
                num |= 0x80;
            }
            if (this.AllowBlank)
            {
                num |= 0x100;
            }
            if (this.SuppressCombo)
            {
                num |= 0x200;
            }
            num |= ((uint) this.ImeMode) << 10;
            if (this.ShowInputMessage)
            {
                num |= 0x40000;
            }
            if (this.ShowErrorMessage)
            {
                num |= 0x80000;
            }
            num |= ((uint) this.ValidationOperator) << 20;
            writer.Write(num);
            this.promptTitle.Write(writer);
            this.errorTitle.Write(writer);
            this.prompt.Write(writer);
            this.error.Write(writer);
            int length = this.formula1Bytes.Length;
            writer.Write(length);
            writer.Write(this.formula1Bytes);
            length = this.formula2Bytes.Length;
            writer.Write(length);
            writer.Write(this.formula2Bytes);
            int count = this.Ranges.Count;
            writer.Write((ushort) count);
            for (int i = 0; i < count; i++)
            {
                this.Ranges[i].Write(writer);
            }
        }

        public XlDataValidationType ValidationType { get; set; }

        public XlDataValidationErrorStyle ErrorStyle { get; set; }

        public XlDataValidationImeMode ImeMode { get; set; }

        public bool StringLookup { get; set; }

        public bool AllowBlank { get; set; }

        public bool SuppressCombo { get; set; }

        public bool ShowInputMessage { get; set; }

        public bool ShowErrorMessage { get; set; }

        public XlDataValidationOperator ValidationOperator { get; set; }

        public string PromptTitle
        {
            get => 
                this.promptTitle.Value.TrimEnd(nullChar);
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.promptTitle.Value = "\0";
                }
                else
                {
                    base.CheckLength(value, 0x20, "PromptTitle");
                    this.promptTitle.Value = value;
                }
            }
        }

        public string ErrorTitle
        {
            get => 
                this.errorTitle.Value.TrimEnd(nullChar);
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.errorTitle.Value = "\0";
                }
                else
                {
                    base.CheckLength(value, 0x20, "ErrorTitle");
                    this.errorTitle.Value = value;
                }
            }
        }

        public string Prompt
        {
            get => 
                this.prompt.Value.TrimEnd(nullChar);
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.prompt.Value = "\0";
                }
                else
                {
                    base.CheckLength(value, 0xff, "Prompt");
                    this.prompt.Value = value;
                }
            }
        }

        public string Error
        {
            get => 
                this.error.Value.TrimEnd(nullChar);
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.error.Value = "\0";
                }
                else
                {
                    base.CheckLength(value, 0xe1, "Error");
                    this.error.Value = value;
                }
            }
        }

        public byte[] Formula1Bytes
        {
            get => 
                this.formula1Bytes;
            set
            {
                if ((value == null) || (value.Length < 2))
                {
                    this.formula1Bytes = new byte[0];
                }
                else
                {
                    int length = BitConverter.ToUInt16(value, 0);
                    this.formula1Bytes = new byte[length];
                    Array.Copy(value, 2, this.formula1Bytes, 0, length);
                }
            }
        }

        public byte[] Formula2Bytes
        {
            get => 
                this.formula2Bytes;
            set
            {
                if ((value == null) || (value.Length < 2))
                {
                    this.formula2Bytes = new byte[0];
                }
                else
                {
                    int length = BitConverter.ToUInt16(value, 0);
                    this.formula2Bytes = new byte[length];
                    Array.Copy(value, 2, this.formula2Bytes, 0, length);
                }
            }
        }

        public IList<XlsRef8> Ranges =>
            this.ranges;
    }
}

