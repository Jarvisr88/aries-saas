namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Editors;
    using System;

    public static class ObjectToTrackBarRangeConverter
    {
        public static TrackBarEditRange TryConvert(object value)
        {
            if (value is IConvertible)
            {
                double num = Convert.ToDouble(value);
                TrackBarEditRange range1 = new TrackBarEditRange();
                range1.SelectionStart = num;
                range1.SelectionEnd = num;
                return range1;
            }
            TrackBarEditRange range = value as TrackBarEditRange;
            if (range == null)
            {
                return new TrackBarEditRange();
            }
            TrackBarEditRange range2 = new TrackBarEditRange();
            range2.SelectionStart = range.SelectionStart;
            range2.SelectionEnd = range.SelectionEnd;
            return range2;
        }
    }
}

