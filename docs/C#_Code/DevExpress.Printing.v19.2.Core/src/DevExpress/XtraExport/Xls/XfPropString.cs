namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class XfPropString : XfPropBase
    {
        private LPWideString stringValue;

        public XfPropString(short typeCode, string value) : base(typeCode)
        {
            this.stringValue = new LPWideString();
            this.Value = value;
        }

        protected override int GetSizeCore() => 
            this.stringValue.Length;

        protected override void WriteCore(BinaryWriter writer)
        {
            this.stringValue.Write(writer);
        }

        public string Value
        {
            get => 
                this.stringValue.Value;
            set => 
                this.stringValue.Value = value;
        }
    }
}

