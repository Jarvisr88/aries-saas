namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class TemplatedBarManagerController : BarManagerController
    {
        public static readonly DependencyProperty TemplateProperty;

        static TemplatedBarManagerController();
        protected override BarManagerActionContainer CreateActionContainer();

        [Description("Gets or sets a DataTemplate that defines a BarManagerActionContainer object storing bar customization actions.This is a dependency property.")]
        public DataTemplate Template { get; set; }
    }
}

