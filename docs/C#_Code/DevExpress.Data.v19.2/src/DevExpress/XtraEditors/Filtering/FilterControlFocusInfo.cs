namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FilterControlFocusInfo
    {
        public readonly DevExpress.XtraEditors.Filtering.Node Node;
        public readonly int ElementIndex;
        public FilterControlFocusInfo(DevExpress.XtraEditors.Filtering.Node node, int elementIndex);
        public static bool operator ==(FilterControlFocusInfo fi1, FilterControlFocusInfo fi2);
        public static bool operator !=(FilterControlFocusInfo fi1, FilterControlFocusInfo fi2);
        public override int GetHashCode();
        public override bool Equals(object obj);
        public FilterControlFocusInfo OnRight();
        public FilterControlFocusInfo OnLeft();
        public FilterControlFocusInfo OnUp();
        public FilterControlFocusInfo OnDown();
        public void ChangeElement(ElementType elementType);
        public void ChangeElement(object value);
        public CriteriaOperator AdditionalOperand { get; }
        public IBoundProperty FocusedFilterProperty { get; }
        public ElementType FocusedElementType { get; }
        public object GetCurrentValue();
    }
}

