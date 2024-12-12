namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class BarItemActionBase : BarManagerControllerActionBase
    {
        public static readonly DependencyProperty ItemNameProperty;
        public static readonly DependencyProperty ItemIndexProperty;

        static BarItemActionBase();
        protected BarItem GetItem();
        public override object GetObjectCore();
        public override bool IsEqual(BarManagerControllerActionBase action);

        [Description("Gets or sets the bar item's name. This is a dependency property.")]
        public string ItemName { get; set; }

        [Description("Gets or sets the index of the current bar item in a bar item collection.This is a dependency property.")]
        public virtual int ItemIndex { get; set; }
    }
}

