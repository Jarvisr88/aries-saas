namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Markup;

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), ContentProperty("Item")]
    public class InsertBarItemAction : BarManagerControllerActionBase
    {
        public static readonly DependencyProperty ItemProperty;
        public static readonly DependencyProperty ItemIndexProperty;

        static InsertBarItemAction();
        protected override void ExecuteCore(DependencyObject context);
        public static int GetItemIndex(DependencyObject d);
        public override object GetObjectCore();
        public override bool IsEqual(BarManagerControllerActionBase action);
        public static void SetItemIndex(DependencyObject d, int value);

        [Description("Gets or sets the bar item inserted in the BarManager.Items collection.This is a dependency property.")]
        public BarItem Item { get; set; }

        [Description("Gets or sets the index at which an item is inserted in the BarManager.Items collection. This is an attached property.")]
        public virtual int ItemIndex { get; set; }
    }
}

