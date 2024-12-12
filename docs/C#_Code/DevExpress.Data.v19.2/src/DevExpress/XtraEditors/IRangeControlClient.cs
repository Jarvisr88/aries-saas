namespace DevExpress.XtraEditors
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public interface IRangeControlClient
    {
        event ClientRangeChangedEventHandler RangeChanged;

        void Calculate(Rectangle contentRect);
        Rectangle CalculateSelectionBounds(RangeControlPaintEventArgs e, Rectangle rect);
        void DrawContent(RangeControlPaintEventArgs e);
        bool DrawRuler(RangeControlPaintEventArgs e);
        void DrawSelection(RangeControlPaintEventArgs e);
        double GetNormalizedValue(object value);
        object GetOptions();
        List<object> GetRuler(RulerInfoArgs e);
        object GetValue(double normalizedValue);
        bool IsValidType(Type type);
        void OnClick(RangeControlHitInfo hitInfo);
        void OnRangeChanged(object rangeMinimum, object rangeMaximum);
        void OnRangeControlChanged(IRangeControl rangeControl);
        void OnResize();
        string RulerToString(int ruleIndex);
        bool SupportOrientation(Orientation orientation);
        void UpdateHotInfo(RangeControlHitInfo hitInfo);
        void UpdatePressedInfo(RangeControlHitInfo hitInfo);
        void ValidateRange(NormalizedRangeInfo info);
        double ValidateScale(double newScale);
        string ValueToString(double normalizedValue);

        int RangeBoxTopIndent { get; }

        int RangeBoxBottomIndent { get; }

        bool IsCustomRuler { get; }

        object RulerDelta { get; }

        double NormalizedRulerDelta { get; }

        bool IsValid { get; }

        string InvalidText { get; }
    }
}

