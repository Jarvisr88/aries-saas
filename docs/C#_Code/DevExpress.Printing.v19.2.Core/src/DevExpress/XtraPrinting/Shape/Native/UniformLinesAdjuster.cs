namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public class UniformLinesAdjuster : ILinesAdjuster
    {
        public static readonly UniformLinesAdjuster Instance = new UniformLinesAdjuster();

        protected UniformLinesAdjuster()
        {
        }

        public ShapeLineCommandCollection AdjustLines(ShapeLineCommand line1, ShapeLineCommand line2)
        {
            ShapeLineCommandCollection commands = new ShapeLineCommandCollection();
            this.FillCommands(commands, line1, line2);
            return commands;
        }

        protected virtual void FillCommands(ShapeLineCommandCollection commands, ShapeLineCommand line1, ShapeLineCommand line2)
        {
            commands.Add(line1);
            commands.Add(line2);
        }
    }
}

