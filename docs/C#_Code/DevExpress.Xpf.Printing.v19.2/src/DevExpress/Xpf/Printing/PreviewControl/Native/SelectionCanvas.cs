﻿namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class SelectionCanvas : Control
    {
        public static readonly DependencyProperty SelectionRectangleProperty;
        private const string PartSelectionCanvas = "PART_SelectionCanvas";

        static SelectionCanvas()
        {
            Type ownerType = typeof(SelectionCanvas);
            SelectionRectangleProperty = DependencyPropertyManager.Register("SelectionRectangle", typeof(DevExpress.Xpf.Printing.PreviewControl.Native.SelectionRectangle), ownerType);
        }

        public SelectionCanvas()
        {
            this.SetDefaultStyleKey(typeof(SelectionCanvas));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Selection = base.GetTemplateChild("PART_SelectionCanvas") as Canvas;
        }

        public DevExpress.Xpf.Printing.PreviewControl.Native.SelectionRectangle SelectionRectangle
        {
            get => 
                (DevExpress.Xpf.Printing.PreviewControl.Native.SelectionRectangle) base.GetValue(SelectionRectangleProperty);
            set => 
                base.SetValue(SelectionRectangleProperty, value);
        }

        private Canvas Selection { get; set; }
    }
}

