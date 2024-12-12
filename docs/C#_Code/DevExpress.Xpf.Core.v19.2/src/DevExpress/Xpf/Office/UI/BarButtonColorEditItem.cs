namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Media;

    [DXToolboxBrowsable(false)]
    public class BarButtonColorEditItem : BarSplitButtonEditItem
    {
        static BarButtonColorEditItem()
        {
            BarItemLinkCreator.Default.RegisterObject(typeof(BarButtonColorEditItem), typeof(BarButtonColorEditItemLink), (CreateObjectMethod<BarItemLink>) (arg => new BarButtonColorEditItemLink()));
        }

        public BarButtonColorEditItem()
        {
            this.InitializePopupControl();
            base.ActAsDropDown = true;
        }

        private void InitializePopupControl()
        {
            ColorEdit edit1 = new ColorEdit();
            edit1.ShowBorder = false;
            edit1.DefaultColor = Color.FromArgb(0, 0, 0, 0);
            ColorEdit edit = edit1;
            Binding binding = new Binding("EditValue");
            binding.Source = this;
            binding.Mode = BindingMode.TwoWay;
            edit.SetBinding(BaseEdit.EditValueProperty, binding);
            PopupControlContainer container1 = new PopupControlContainer();
            container1.Content = edit;
            base.PopupControl = container1;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarButtonColorEditItem.<>c <>9 = new BarButtonColorEditItem.<>c();

            internal BarItemLink <.cctor>b__0_0(object arg) => 
                new BarButtonColorEditItemLink();
        }
    }
}

