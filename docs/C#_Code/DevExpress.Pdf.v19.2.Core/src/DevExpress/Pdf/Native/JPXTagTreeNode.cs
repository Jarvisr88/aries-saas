namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXTagTreeNode
    {
        private readonly bool isDefined;
        private readonly int value;

        public JPXTagTreeNode(int value, bool isDefined)
        {
            this.value = value;
            this.isDefined = isDefined;
        }

        public bool IsDefined =>
            this.isDefined;

        public int Value =>
            this.value;
    }
}

