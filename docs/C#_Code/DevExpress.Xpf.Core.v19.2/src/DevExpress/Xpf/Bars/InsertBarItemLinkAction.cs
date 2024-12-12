namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("ItemLink"), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public class InsertBarItemLinkAction : BarItemLinkActionBase
    {
        public static readonly DependencyProperty ItemLinkProperty;

        static InsertBarItemLinkAction();
        protected override void ExecuteCore(DependencyObject context);
        public override object GetObjectCore();
        public override bool IsEqual(BarManagerControllerActionBase action);

        [Description("Gets or sets the bar item link inserted at a specific position (BarItemLinkActionBase.ItemLinkIndex) within a target object's item link collection.This is a dependency property.")]
        public BarItemLinkBase ItemLink { get; set; }
    }
}

