namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office.Drawing;
    using System;
    using System.Collections.Generic;

    public class AdjustableCoordinateCache : Dictionary<string, AdjustableCoordinate>
    {
        public AdjustableCoordinate GetCachedAdjustableCoordinate(string value)
        {
            AdjustableCoordinate coordinate;
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            if (!base.TryGetValue(value, out coordinate))
            {
                coordinate = AdjustableCoordinate.FromString(value);
                if (coordinate != null)
                {
                    base.Add(value, coordinate);
                }
            }
            return coordinate;
        }
    }
}

