namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentDefinedName : XlsContentBase
    {
        private const int fixedPartSize = 14;
        private static readonly string[] builtInNames;
        private int functionCategory;
        private int sheetIndex;
        private XLUnicodeStringNoCch name = new XLUnicodeStringNoCch();
        private byte[] formulaBytes = new byte[2];
        private int formulaSize;

        static XlsContentDefinedName()
        {
            string[] textArray1 = new string[14];
            textArray1[0] = "_xlnm.Consolidate_Area";
            textArray1[1] = "_xlnm.Auto_Open";
            textArray1[2] = "_xlnm.Auto_Close";
            textArray1[3] = "_xlnm.Extract";
            textArray1[4] = "_xlnm.Database";
            textArray1[5] = "_xlnm.Criteria";
            textArray1[6] = "_xlnm.Print_Area";
            textArray1[7] = "_xlnm.Print_Titles";
            textArray1[8] = "_xlnm.Recorder";
            textArray1[9] = "_xlnm.Data_Form";
            textArray1[10] = "_xlnm.Auto_Activate";
            textArray1[11] = "_xlnm.Auto_Deactivate";
            textArray1[12] = "_xlnm.Sheet_Title";
            textArray1[13] = "_xlnm._FilterDatabase";
            builtInNames = textArray1;
        }

        internal static string GetName(string value, bool isBuiltIn)
        {
            if (!isBuiltIn)
            {
                return value;
            }
            if (value.Length == 1)
            {
                int index = Convert.ToUInt16(value[0]);
                if ((index >= 0) && (index < builtInNames.Length))
                {
                    return builtInNames[index];
                }
            }
            return ("_xlnm." + value);
        }

        public override int GetSize() => 
            (this.GetSizeWithoutFormula() + this.formulaBytes.Length) - 2;

        private int GetSizeWithoutFormula() => 
            14 + this.name.Length;

        public override void Read(XlReader reader, int size)
        {
            ushort num = reader.ReadUInt16();
            this.IsHidden = Convert.ToBoolean((int) (num & 1));
            this.IsXlmMacro = Convert.ToBoolean((int) (num & 2));
            this.IsVbaMacro = Convert.ToBoolean((int) (num & 4));
            this.IsMacro = Convert.ToBoolean((int) (num & 8));
            this.CanReturnArray = Convert.ToBoolean((int) (num & 0x10));
            this.IsBuiltIn = Convert.ToBoolean((int) (num & 0x20));
            this.FunctionCategory = (num & 0xfc0) >> 6;
            this.IsPublished = Convert.ToBoolean((int) (num & 0x2000));
            this.IsWorkbookParameter = Convert.ToBoolean((int) (num & 0x4000));
            this.Key = reader.ReadByte();
            int charCount = reader.ReadByte();
            this.formulaSize = reader.ReadUInt16();
            reader.ReadUInt16();
            this.SheetIndex = reader.ReadUInt16();
            reader.ReadUInt32();
            this.name = XLUnicodeStringNoCch.FromStream(reader, charCount);
            if (this.formulaSize > 0)
            {
                int count = size - this.GetSizeWithoutFormula();
                this.formulaBytes = reader.ReadBytes(count);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            ushort num = 0;
            if (this.IsHidden)
            {
                num = (ushort) (num | 1);
            }
            if (this.IsXlmMacro)
            {
                num = (ushort) (num | 2);
            }
            if (this.IsVbaMacro)
            {
                num = (ushort) (num | 4);
            }
            if (this.IsMacro)
            {
                num = (ushort) (num | 8);
            }
            if (this.CanReturnArray)
            {
                num = (ushort) (num | 0x10);
            }
            if (this.IsBuiltIn)
            {
                num = (ushort) (num | 0x20);
            }
            num = (ushort) (num | ((ushort) ((this.FunctionCategory & 0x3f) << 6)));
            if (this.IsPublished)
            {
                num = (ushort) (num | 0x2000);
            }
            if (this.IsWorkbookParameter)
            {
                num = (ushort) (num | 0x4000);
            }
            writer.Write(num);
            writer.Write(this.Key);
            writer.Write((byte) this.name.Value.Length);
            writer.Write(BitConverter.ToUInt16(this.formulaBytes, 0));
            writer.Write((ushort) 0);
            writer.Write((ushort) this.SheetIndex);
            writer.Write((uint) 0);
            this.name.Write(writer);
            if (this.formulaBytes.Length > 2)
            {
                writer.Write(this.formulaBytes, 2, this.formulaBytes.Length - 2);
            }
        }

        public bool IsHidden { get; set; }

        public bool IsXlmMacro { get; set; }

        public bool IsVbaMacro { get; set; }

        public bool IsMacro { get; set; }

        public bool CanReturnArray { get; set; }

        public bool IsBuiltIn { get; protected set; }

        public int FunctionCategory
        {
            get => 
                this.functionCategory;
            set
            {
                base.CheckValue(value, 0, 0x1f, "FunctionCategory");
                this.functionCategory = value;
            }
        }

        public bool IsPublished { get; set; }

        public bool IsWorkbookParameter { get; set; }

        public byte Key { get; set; }

        public int SheetIndex
        {
            get => 
                this.sheetIndex;
            set
            {
                base.CheckValue(value, 0, 0xffff, "SheetIndex");
                this.sheetIndex = value;
            }
        }

        public string Name
        {
            get
            {
                if (!this.IsBuiltIn)
                {
                    return this.name.Value;
                }
                if (this.name.Value.Length == 1)
                {
                    int index = Convert.ToUInt16(this.name.Value[0]);
                    if ((index >= 0) && (index < builtInNames.Length))
                    {
                        return builtInNames[index];
                    }
                }
                return ("_xlnm." + this.name.Value);
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.StartsWith("_xlnm."))
                {
                    int num = -1;
                    int index = 0;
                    while (true)
                    {
                        if (index < builtInNames.Length)
                        {
                            if (value != builtInNames[index])
                            {
                                index++;
                                continue;
                            }
                            num = index;
                        }
                        if (num == -1)
                        {
                            break;
                        }
                        this.name.Value = char.ToString((char) num);
                        this.IsBuiltIn = true;
                        return;
                    }
                }
                this.name.Value = value;
                this.IsBuiltIn = false;
            }
        }

        public string Comment { get; set; }

        public string InternalName =>
            this.name.Value;

        public byte[] FormulaBytes
        {
            get => 
                this.formulaBytes;
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    this.formulaBytes = new byte[2];
                    this.formulaSize = 0;
                }
                else
                {
                    if (value.Length < 2)
                    {
                        throw new ArgumentException("value must be at least 2 bytes long");
                    }
                    this.formulaBytes = value;
                    this.formulaSize = BitConverter.ToUInt16(this.formulaBytes, 0);
                }
            }
        }

        public int FormulaSize
        {
            get => 
                this.formulaSize;
            set => 
                this.formulaSize = value;
        }

        protected string[] BuiltInNames =>
            builtInNames;
    }
}

