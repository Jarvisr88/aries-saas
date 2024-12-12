namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public class BarActionBase : BarManagerControllerActionBase
    {
        public static readonly DependencyProperty BarNameProperty;
        public static readonly DependencyProperty BarIndexProperty;

        static BarActionBase();
        protected Bar GetBar();
        public override object GetObjectCore();
        public override bool IsEqual(BarManagerControllerActionBase action);

        [Description("Gets or sets the name of a bar.This is a dependency property.")]
        public string BarName { get; set; }

        [Description("Gets or sets the index of the current bar in a bar collection.This is a dependency property.")]
        public virtual int BarIndex { get; set; }
    }
}

