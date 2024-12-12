namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public class ShortestLineLinesAdjuster : UniformLinesAdjuster
    {
        public static readonly ShortestLineLinesAdjuster Instance = new ShortestLineLinesAdjuster();

        protected ShortestLineLinesAdjuster()
        {
        }

        protected override void FillCommands(ShapeLineCommandCollection commands, ShapeLineCommand line1, ShapeLineCommand line2)
        {
            float num = ((line1.Length == 0f) || (line2.Length == 0f)) ? 1f : (line1.Length / line2.Length);
            if (line1.Length < line2.Length)
            {
                FillCommandsWithRatios(commands, line1, line2, 1f, num);
            }
            else
            {
                FillCommandsWithRatios(commands, line1, line2, 1f / num, 1f);
            }
        }

        private static void FillCommandsWithRatios(ShapeLineCommandCollection commands, ShapeLineCommand line1, ShapeLineCommand line2, float ratio1, float ratio2)
        {
            PointF startPoint = ConsecutiveLineCommandsRounder.DividePoints(line1.StartPoint, line1.EndPoint, ratio1);
            commands.AddLine(startPoint, line1.EndPoint);
            PointF endPoint = ConsecutiveLineCommandsRounder.DividePoints(line2.EndPoint, line2.StartPoint, ratio2);
            commands.AddLine(line2.StartPoint, endPoint);
        }
    }
}

