namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data.Filtering;
    using System;

    public class NodeEditableElement
    {
        private DevExpress.XtraEditors.Filtering.Node node;
        private DevExpress.XtraEditors.Filtering.ElementType elementType;
        private string text;
        private string textBefore;
        private string textAfter;
        private bool isEmpty;
        private int valueIndex;

        public NodeEditableElement(DevExpress.XtraEditors.Filtering.Node node, DevExpress.XtraEditors.Filtering.ElementType elementType, string text);
        public FilterControlFocusInfo CreateFocusInfo();
        public override string ToString();

        public DevExpress.XtraEditors.Filtering.ElementType ElementType { get; }

        public string Text { get; }

        public DevExpress.XtraEditors.Filtering.Node Node { get; }

        public int Index { get; }

        public string TextBefore { get; set; }

        public string TextAfter { get; set; }

        public bool IsEmpty { get; set; }

        public int ValueIndex { get; set; }

        public bool IsValueElement { get; }

        public CriteriaOperator AdditionalOperand { get; }
    }
}

