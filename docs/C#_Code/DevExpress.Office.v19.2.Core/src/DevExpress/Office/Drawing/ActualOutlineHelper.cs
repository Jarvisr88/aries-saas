namespace DevExpress.Office.Drawing
{
    using System;

    internal static class ActualOutlineHelper
    {
        public static OutlineInfo MergeOutline(Outline shapeOutline, Outline themeOutline)
        {
            if (themeOutline == null)
            {
                return shapeOutline.Info;
            }
            if (shapeOutline.IsDefault)
            {
                return themeOutline.Info;
            }
            return new OutlineInfo { 
                Width = (!shapeOutline.HasWidth || (shapeOutline.Width <= 0)) ? themeOutline.Width : shapeOutline.Width,
                CompoundType = shapeOutline.HasCompoundType ? shapeOutline.CompoundType : themeOutline.CompoundType,
                Dashing = shapeOutline.HasDashing ? shapeOutline.Dashing : themeOutline.Dashing,
                JoinStyle = shapeOutline.HasLineJoinStyle ? shapeOutline.JoinStyle : themeOutline.JoinStyle,
                MiterLimit = shapeOutline.HasMiterLimit ? shapeOutline.MiterLimit : themeOutline.MiterLimit,
                StrokeAlignment = shapeOutline.HasStrokeAlignment ? shapeOutline.StrokeAlignment : themeOutline.StrokeAlignment,
                EndCapStyle = shapeOutline.HasEndCapStyle ? shapeOutline.EndCapStyle : themeOutline.EndCapStyle,
                HeadType = shapeOutline.HasHeadType ? shapeOutline.HeadType : themeOutline.HeadType,
                HeadLength = shapeOutline.HasHeadLength ? shapeOutline.HeadLength : themeOutline.HeadLength,
                HeadWidth = shapeOutline.HasHeadWidth ? shapeOutline.HeadWidth : themeOutline.HeadWidth,
                TailType = shapeOutline.HasTailType ? shapeOutline.TailType : themeOutline.TailType,
                TailLength = shapeOutline.HasTailLength ? shapeOutline.TailLength : themeOutline.TailLength,
                TailWidth = shapeOutline.HasTailWidth ? shapeOutline.TailWidth : themeOutline.TailWidth
            };
        }
    }
}

