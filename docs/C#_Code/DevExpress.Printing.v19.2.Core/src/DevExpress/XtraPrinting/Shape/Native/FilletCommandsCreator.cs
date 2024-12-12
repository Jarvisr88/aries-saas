namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public class FilletCommandsCreator : IClosedListVisitor
    {
        private ShapeBezierCommandCollection filletCommands = new ShapeBezierCommandCollection();
        private ILinesAdjuster adjuster;
        private float filletInPercents;

        public FilletCommandsCreator(float filletInPercents, ILinesAdjuster adjuster)
        {
            this.adjuster = adjuster;
            this.filletInPercents = filletInPercents;
        }

        void IClosedListVisitor.VisitElement(object previous, object current, int currentObjectIndex)
        {
            this.filletCommands.Add(ConsecutiveLineCommandsRounder.CreateFilletCommand((ShapeLineCommand) previous, (ShapeLineCommand) current, this.filletInPercents, this.adjuster));
        }

        public ShapeBezierCommandCollection FilletCommands =>
            this.filletCommands;
    }
}

