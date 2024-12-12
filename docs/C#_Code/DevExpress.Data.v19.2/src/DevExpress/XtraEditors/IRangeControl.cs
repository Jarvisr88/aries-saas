namespace DevExpress.XtraEditors
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public interface IRangeControl
    {
        int CalcX(double normalizedValue);
        void CenterSelectedRange();
        double ConstrainRangeMaximum(double value);
        double ConstrainRangeMinimum(double value);
        bool IsValidValue(object value);
        void OnRangeMaximumChanged(object range);
        void OnRangeMinimumChanged(object range);

        Color BorderColor { get; }

        Color RulerColor { get; }

        Color LabelColor { get; }

        Matrix NormalTransform { get; }

        double VisibleRangeStartPosition { get; }

        double VisibleRangeWidth { get; }

        RangeControlRange SelectedRange { get; set; }

        bool AnimateOnDataChange { get; set; }

        IRangeControlClient Client { get; set; }
    }
}

