namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows.Input;

    [DXToolboxBrowsable(false)]
    public class SelectAllItemCheckEdit : CheckEdit
    {
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
        }

        private CheckEditStrategy EditStrategy =>
            (CheckEditStrategy) base.EditStrategy;
    }
}

