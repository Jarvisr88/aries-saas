namespace DevExpress.XtraPrinting.Shape.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Shape;
    using System;
    using System.Drawing;

    public static class ShapeHelper
    {
        public const int TopAngle = 0;
        public const int LeftAngle = 90;
        public const int RightAngle = 270;
        public const int BottomAngle = 180;
        public const double WholeCircleInRad = 6.2831853071795862;
        public const int WholeCircleInDeg = 360;
        public const int HalfOfCircleInDeg = 180;

        public static ShapeCommandCollection CreateBraceCommands(RectangleF bounds, int tailLength, int tipLength, int fillet)
        {
            float height = bounds.Height;
            float num2 = height / 2f;
            float num3 = height * (((float) tailLength) / 100f);
            float num4 = height * (((float) tipLength) / 100f);
            PointF end = RectHelper.CenterOf(bounds);
            float y = end.Y - num2;
            ShapeLineCommand command = new ShapeLineCommand(new PointF(end.X + num4, y), new PointF(end.X, y));
            ShapeLineCommand command2 = new ShapeLineCommand(command.EndPoint, ConsecutiveLineCommandsRounder.DividePoints(command.EndPoint, end, 0.5f));
            ShapeLineCommand command3 = new ShapeLineCommand(command2.EndPoint, end);
            ShapeLineCommand command4 = new ShapeLineCommand(command3.EndPoint, new PointF(end.X - num3, end.Y));
            ShapeBezierCommand filletCommand = ConsecutiveLineCommandsRounder.CreateShortestLineFilletCommand(command, command2, (float) (2 * fillet));
            ShapeBezierCommand command6 = ConsecutiveLineCommandsRounder.CreateShortestLineFilletCommand(command3, command4, (float) (2 * fillet));
            LineCommandsRounder.RoundLineCommands(command3, command4, command6);
            LineCommandsRounder.RoundLineCommands(command, command2, filletCommand);
            float num6 = end.Y + num2;
            ShapeLineCommand command7 = new ShapeLineCommand(new PointF(end.X - num3, end.Y), end);
            ShapeLineCommand command8 = new ShapeLineCommand(command7.EndPoint, ConsecutiveLineCommandsRounder.DividePoints(command7.EndPoint, new PointF(end.X, num6), 0.5f));
            ShapeLineCommand command9 = new ShapeLineCommand(command8.EndPoint, new PointF(end.X, num6));
            ShapeLineCommand command10 = new ShapeLineCommand(command9.EndPoint, new PointF(end.X + num4, num6));
            ShapeBezierCommand command11 = ConsecutiveLineCommandsRounder.CreateShortestLineFilletCommand(command7, command8, (float) (2 * fillet));
            ShapeBezierCommand command12 = ConsecutiveLineCommandsRounder.CreateShortestLineFilletCommand(command9, command10, (float) (2 * fillet));
            LineCommandsRounder.RoundLineCommands(command7, command8, command11);
            LineCommandsRounder.RoundLineCommands(command9, command10, command12);
            ShapePointsCommandCollection commands = new ShapePointsCommandCollection();
            commands.Add(command);
            commands.Add(filletCommand);
            commands.Add(command2);
            commands.Add(command3);
            commands.Add(command6);
            commands.Add(command4);
            commands.Add(command7);
            commands.Add(command11);
            commands.Add(command8);
            commands.Add(command9);
            commands.Add(command12);
            commands.Add(command10);
            ShapePathCommandCollection commands2 = new ShapePathCommandCollection();
            commands2.Add(new ShapeDrawPathCommand(commands));
            return commands2;
        }

        public static ShapeCommandCollection CreateCommands(ShapeBase shape, RectangleF bounds, int angle) => 
            shape.CreateCommands(bounds, angle);

        public static ShapeLineCommandCollection CreateConsecutiveLinesFromPoints(PointF[] points)
        {
            ConsecutiveLinesCreator visitor = new ConsecutiveLinesCreator();
            ClosedListIterator.Iterate(points, visitor);
            return visitor.Lines;
        }

        public static PointF[] CreatePoints(ClosedShapeBase shape, RectangleF bounds, int angle) => 
            shape.CreatePoints(bounds, angle);

        public static PointF[] CreateRectanglePoints(RectangleF bounds) => 
            new PointF[] { new PointF(bounds.Right, bounds.Top), new PointF(bounds.Right, bounds.Bottom), new PointF(bounds.Left, bounds.Bottom), bounds.Location };

        public static float DegToRad(float angleInDeg) => 
            0.01745329f * angleInDeg;

        public static void DrawShapeContent(ShapeBase shape, IGraphics gr, RectangleF clientBounds, IShapeDrawingInfo info)
        {
            shape.DrawContent(gr, clientBounds, info);
        }

        public static float PercentsToRatio(int percents) => 
            PercentsToRatio((float) percents);

        public static float PercentsToRatio(float percents) => 
            percents / 100f;

        public static float RadToDeg(float angleInRad) => 
            57.29578f * angleInRad;

        public static bool SupportsFillColor(ShapeBase shape) => 
            shape.SupportsFillColor;

        public static int ValidateAngle(int value)
        {
            int num = value % 360;
            if (num < 0)
            {
                num += 360;
            }
            return num;
        }

        public static int ValidatePercentageValue(int value, string message) => 
            ValidateRestrictedValue(value, 0, 100, message);

        public static float ValidatePercentageValue(float value, string message) => 
            ValidateRestrictedValue(value, 0f, 100f, message);

        public static int ValidateRestrictedValue(int value, int minValue, int maxValue, string message)
        {
            if ((value < minValue) || (maxValue < value))
            {
                throw new ArgumentOutOfRangeException(message);
            }
            return value;
        }

        public static float ValidateRestrictedValue(float value, float minValue, float maxValue, string message)
        {
            if ((value < minValue) || (maxValue < value))
            {
                throw new ArgumentOutOfRangeException(message);
            }
            return value;
        }
    }
}

