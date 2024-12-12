namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public static class ConsecutiveLineCommandsRounder
    {
        private const float FilletCorrectFactor = 0.45f;

        private static PointF CalcBasePoint(PointF start, PointF end, float fillet) => 
            DividePoints(DividePoints(start, end, 0.5f), end, fillet);

        private static PointF CalcControlPoint(PointF start, PointF end, float fillet) => 
            CalcBasePoint(start, end, fillet * 0.45f);

        public static ShapeBezierCommand CreateFilletCommand(ShapeLineCommand line1, ShapeLineCommand line2, float filletInPercents, ILinesAdjuster adjuster)
        {
            float fillet = ShapeHelper.PercentsToRatio(filletInPercents);
            ShapeLineCommandCollection commands = adjuster.AdjustLines(line1, line2);
            ShapeLineCommand command = (ShapeLineCommand) commands[0];
            ShapeLineCommand command2 = (ShapeLineCommand) commands[1];
            return new ShapeBezierCommand(CalcBasePoint(command.StartPoint, command.EndPoint, fillet), CalcControlPoint(command.StartPoint, command.EndPoint, fillet), CalcControlPoint(command2.EndPoint, command2.StartPoint, fillet), CalcBasePoint(command2.EndPoint, command2.StartPoint, fillet));
        }

        public static ShapePointsCommandCollection CreateRoundedConsecutiveLinesCommands(ShapeCommandCollection lineCommands, float filletInPercents, ILinesAdjuster adjuster)
        {
            FilletCommandsCreator visitor = new FilletCommandsCreator(filletInPercents, adjuster);
            ClosedListIterator.Iterate(lineCommands, visitor);
            LineCommandsRounder rounder = new LineCommandsRounder(visitor.FilletCommands);
            ClosedListIterator.Iterate(lineCommands, rounder);
            int count = lineCommands.Count;
            ShapePointsCommandCollection commands = new ShapePointsCommandCollection();
            for (int i = 0; i < count; i++)
            {
                commands.Add(visitor.FilletCommands[i]);
                commands.Add(lineCommands[i]);
            }
            return commands;
        }

        public static ShapeBezierCommand CreateShortestLineFilletCommand(ShapeLineCommand line1, ShapeLineCommand line2, float filletInPercents) => 
            CreateFilletCommand(line1, line2, filletInPercents, ShortestLineLinesAdjuster.Instance);

        public static ShapeBezierCommand CreateUniformlyFilletCommand(ShapeLineCommand line1, ShapeLineCommand line2, float filletInPercents) => 
            CreateFilletCommand(line1, line2, filletInPercents, UniformLinesAdjuster.Instance);

        public static PointF DividePoints(PointF start, PointF end, float divideFactorFromEnd) => 
            new PointF(DivideValues(start.X, end.X, divideFactorFromEnd), DivideValues(start.Y, end.Y, divideFactorFromEnd));

        private static float DivideValues(float start, float end, float divideFactorFromEnd) => 
            (divideFactorFromEnd * start) + ((1f - divideFactorFromEnd) * end);
    }
}

