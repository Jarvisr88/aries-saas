namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class InplaceErrorControl : FrameworkElement
    {
        private Geometry CreateGeometry() => 
            Geometry.Parse("F1 M 1265.5,761C 1270.19,761 1274,764.806 1274,769.5C 1274,774.194 1270.19,778 1265.5,778C 1260.81,778 1257,774.194 1257,769.5C 1257,764.806 1260.81,761 1265.5,761 Z ");

        protected override Size MeasureOverride(Size availableSize) => 
            new Size(17.0, 17.0);

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(Brushes.Red, new Pen(Brushes.Green, 2.0), new Rect(0.0, 0.0, 17.0, 17.0));
        }

        public BaseValidationError Error { get; set; }
    }
}

