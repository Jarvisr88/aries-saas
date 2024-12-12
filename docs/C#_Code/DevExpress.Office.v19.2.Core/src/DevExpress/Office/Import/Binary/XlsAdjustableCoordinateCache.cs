namespace DevExpress.Office.Import.Binary
{
    using DevExpress.Office.Drawing;
    using System;
    using System.Collections.Generic;

    internal class XlsAdjustableCoordinateCache
    {
        private readonly Dictionary<int, AdjustableCoordinate> intValues = new Dictionary<int, AdjustableCoordinate>();
        private readonly Dictionary<string, AdjustableCoordinate> stringValues = new Dictionary<string, AdjustableCoordinate>();

        public AdjustableCoordinate GetCachedAdjustableCoordinate(int value)
        {
            AdjustableCoordinate coordinate;
            if (!this.intValues.TryGetValue(value, out coordinate))
            {
                coordinate = new AdjustableCoordinate((double) value);
                this.intValues.Add(value, coordinate);
            }
            return coordinate;
        }

        public AdjustableCoordinate GetCachedAdjustableCoordinate(string value)
        {
            AdjustableCoordinate coordinate;
            if (string.IsNullOrEmpty(value))
            {
                return this.GetCachedAdjustableCoordinate(0);
            }
            if (!this.stringValues.TryGetValue(value, out coordinate))
            {
                coordinate = AdjustableCoordinate.FromString(value);
                if (coordinate != null)
                {
                    this.stringValues.Add(value, coordinate);
                }
            }
            return coordinate;
        }
    }
}

