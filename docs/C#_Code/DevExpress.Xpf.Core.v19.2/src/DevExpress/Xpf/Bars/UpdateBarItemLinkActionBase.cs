namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class UpdateBarItemLinkActionBase : BarItemLinkActionBase
    {
        public static readonly DependencyProperty ItemLinkNameProperty;

        static UpdateBarItemLinkActionBase();
        protected BarItemLinkBase GetLink();
        public override bool IsEqual(BarManagerControllerActionBase action);

        [Description("Gets or sets the name of the bar item link.This is a dependency property.")]
        public string ItemLinkName { get; set; }
    }
}

