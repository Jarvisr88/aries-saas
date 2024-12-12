namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office.Drawing;
    using System;
    using System.Collections.Generic;

    public class AdjustableAngleCache : Dictionary<string, AdjustableAngle>
    {
        public AdjustableAngle GetCachedAdjustableAngle(string value)
        {
            AdjustableAngle angle;
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            if (!base.TryGetValue(value, out angle))
            {
                angle = AdjustableAngle.FromString(value);
                if (angle != null)
                {
                    base.Add(value, angle);
                }
            }
            return angle;
        }
    }
}

