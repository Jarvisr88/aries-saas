namespace DevExpress.Office.Import.Binary
{
    using DevExpress.Office.Drawing;
    using System;
    using System.Collections.Generic;

    internal class XlsAdjustableAngleCache : Dictionary<int, AdjustableAngle>
    {
        public AdjustableAngle GetCachedAdjustableAngle(int value)
        {
            AdjustableAngle angle;
            if (!base.TryGetValue(value, out angle))
            {
                angle = new AdjustableAngle((double) value);
                base.Add(value, angle);
            }
            return angle;
        }
    }
}

