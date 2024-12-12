namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class XlsContentList12DisplayName : XlsContentList12Base
    {
        private XLUnicodeString displayName = new XLUnicodeString();
        private XLUnicodeString comment = new XLUnicodeString();

        public override int GetSize() => 
            (base.GetSize() + this.displayName.Length) + this.comment.Length;

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            this.displayName.Write(writer);
            this.comment.Write(writer);
        }

        protected override short DataType =>
            2;

        public string DisplayName
        {
            get => 
                this.displayName.Value;
            set => 
                this.displayName.Value = value;
        }

        public string Comment
        {
            get => 
                this.comment.Value;
            set => 
                this.comment.Value = value;
        }
    }
}

