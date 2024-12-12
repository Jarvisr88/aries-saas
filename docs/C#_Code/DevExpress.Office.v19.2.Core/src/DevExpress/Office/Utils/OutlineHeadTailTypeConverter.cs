namespace DevExpress.Office.Utils
{
    using DevExpress.Office.Drawing;
    using System;

    public static class OutlineHeadTailTypeConverter
    {
        public static MsoLineEnd GetMsoLineEnd(OutlineHeadTailType outlineHeadTailType)
        {
            switch (outlineHeadTailType)
            {
                case OutlineHeadTailType.Arrow:
                    return MsoLineEnd.OpenEnd;

                case OutlineHeadTailType.Diamond:
                    return MsoLineEnd.DiamondEnd;

                case OutlineHeadTailType.Oval:
                    return MsoLineEnd.OvalEnd;

                case OutlineHeadTailType.StealthArrow:
                    return MsoLineEnd.StealthEnd;

                case OutlineHeadTailType.TriangleArrow:
                    return MsoLineEnd.ArrowEnd;
            }
            return MsoLineEnd.NoEnd;
        }

        public static OutlineHeadTailType GetOutlineHeadTailType(MsoLineEnd msoLineEnd)
        {
            switch (msoLineEnd)
            {
                case MsoLineEnd.ArrowEnd:
                    return OutlineHeadTailType.TriangleArrow;

                case MsoLineEnd.StealthEnd:
                    return OutlineHeadTailType.StealthArrow;

                case MsoLineEnd.DiamondEnd:
                    return OutlineHeadTailType.Diamond;

                case MsoLineEnd.OvalEnd:
                    return OutlineHeadTailType.Oval;

                case MsoLineEnd.OpenEnd:
                    return OutlineHeadTailType.Arrow;
            }
            return OutlineHeadTailType.None;
        }
    }
}

