namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public abstract class ShapePathCommand : ShapeCommandBase
    {
        private ShapePointsCommandCollection commands;

        protected ShapePathCommand(ShapePointsCommandCollection commands)
        {
            this.commands = commands;
        }

        public ShapePointsCommandCollection Commands =>
            this.commands;

        public bool IsClosed
        {
            get
            {
                if (this.commands.Count == 0)
                {
                    return false;
                }
                ShapeLineCommand command2 = (ShapeLineCommand) this.commands[this.commands.Count - 1];
                return (((ShapeLineCommand) this.commands[0]).StartPoint == command2.EndPoint);
            }
        }
    }
}

