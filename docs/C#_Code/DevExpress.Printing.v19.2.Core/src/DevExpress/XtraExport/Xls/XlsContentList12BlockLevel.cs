namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class XlsContentList12BlockLevel : XlsContentList12Base
    {
        private XlsDxfN12List header;
        private XlsDxfN12List data;
        private XlsDxfN12List total;
        private XlsDxfN12List border;
        private XlsDxfN12List headerBorder;
        private XlsDxfN12List totalBorder;

        public XlsContentList12BlockLevel()
        {
            XlsDxfN12List list1 = new XlsDxfN12List();
            list1.IsEmpty = true;
            this.header = list1;
            XlsDxfN12List list2 = new XlsDxfN12List();
            list2.IsEmpty = true;
            this.data = list2;
            XlsDxfN12List list3 = new XlsDxfN12List();
            list3.IsEmpty = true;
            this.total = list3;
            XlsDxfN12List list4 = new XlsDxfN12List();
            list4.IsEmpty = true;
            this.border = list4;
            XlsDxfN12List list5 = new XlsDxfN12List();
            list5.IsEmpty = true;
            this.headerBorder = list5;
            XlsDxfN12List list6 = new XlsDxfN12List();
            list6.IsEmpty = true;
            this.totalBorder = list6;
        }

        public override int GetSize() => 
            ((((((base.GetSize() + 0x24) + this.header.GetSize()) + this.data.GetSize()) + this.total.GetSize()) + this.border.GetSize()) + this.headerBorder.GetSize()) + this.totalBorder.GetSize();

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write((int) this.header.GetSize());
            writer.Write(-1);
            writer.Write((int) this.data.GetSize());
            writer.Write(-1);
            writer.Write((int) this.total.GetSize());
            writer.Write(-1);
            writer.Write((int) this.border.GetSize());
            writer.Write((int) this.headerBorder.GetSize());
            writer.Write((int) this.totalBorder.GetSize());
            this.header.Write(writer);
            this.data.Write(writer);
            this.total.Write(writer);
            this.border.Write(writer);
            this.headerBorder.Write(writer);
            this.totalBorder.Write(writer);
        }

        protected override short DataType =>
            0;

        public XlsDxfN12List Header =>
            this.header;

        public XlsDxfN12List Data =>
            this.data;

        public XlsDxfN12List Total =>
            this.total;

        public XlsDxfN12List Border =>
            this.border;

        public XlsDxfN12List HeaderBorder =>
            this.headerBorder;

        public XlsDxfN12List TotalBorder =>
            this.totalBorder;
    }
}

