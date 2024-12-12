namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class BrickExtensions
    {
        public static string AsText(this BrickBase brick)
        {
            if (brick == null)
            {
                return "null";
            }
            TextBrickBase base2 = brick as TextBrickBase;
            return $"{brick.GetType().Name} Text="{((base2 != null) ? base2.Text : "")}" Rect={brick.Rect}";
        }

        public static float GetRightOnPage(this Brick brick) => 
            brick.InitialRect.Right + brick.PageBuilderOffset.X;
    }
}

