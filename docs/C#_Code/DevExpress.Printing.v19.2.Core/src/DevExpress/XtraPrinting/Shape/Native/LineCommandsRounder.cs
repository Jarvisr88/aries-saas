namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public class LineCommandsRounder : IClosedListVisitor
    {
        private ShapeBezierCommandCollection filletCommands;

        public LineCommandsRounder(ShapeBezierCommandCollection filletCommands)
        {
            this.filletCommands = filletCommands;
        }

        void IClosedListVisitor.VisitElement(object previous, object current, int currentObjectIndex)
        {
            RoundLineCommands((ShapeLineCommand) previous, (ShapeLineCommand) current, (ShapeBezierCommand) this.filletCommands[currentObjectIndex]);
        }

        public static void RoundLineCommands(ShapeLineCommand line1, ShapeLineCommand line2, ShapeBezierCommand filletCommand)
        {
            line1.EndPoint = filletCommand.StartPoint;
            line2.StartPoint = filletCommand.EndPoint;
        }
    }
}

