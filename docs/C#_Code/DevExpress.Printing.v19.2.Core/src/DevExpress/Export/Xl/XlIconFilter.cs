namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlIconFilter : IXlFilter, IXlFilterCriteria
    {
        public XlIconFilter(XlCondFmtIconSetType iconSet, int iconId)
        {
            this.IconSet = iconSet;
            if ((iconId < -1) || (iconId >= XlCondFmtRuleIconSet.IconSetCountTable[iconSet]))
            {
                throw new ArgumentOutOfRangeException("iconId out of range.");
            }
            this.IconId = iconId;
        }

        bool IXlFilter.MeetCriteria(IXlCell cell, IXlCellFormatter cellFormatter) => 
            true;

        public XlCondFmtIconSetType IconSet { get; private set; }

        public int IconId { get; private set; }

        public XlFilterType FilterType =>
            XlFilterType.Icon;
    }
}

