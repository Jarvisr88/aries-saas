namespace DevExpress.Data.Svg
{
    using System;
    using System.Text;

    public abstract class SvgCommandBase : ISvgInstance
    {
        private SvgPointCollection points;

        public SvgCommandBase();
        public SvgCommandBase(params SvgPoint[] initialPoints);
        protected void CalculateAbsolutePoints(SvgPoint prevPoint);
        public void ExportCommand(StringBuilder stringBuilder);
        protected virtual void ExportCommandParameters(StringBuilder stringBuilder);
        public void FillCommand(string[] commandsElementsList, int index, SvgPoint prevPoint);
        protected virtual SvgPointCollection ParsePoints(string[] commandsElementsList, int index, SvgPoint prevPoint);

        public SvgPointCollection Points { get; protected internal set; }

        public virtual bool IgnoreChildren { get; }

        public virtual bool IsRelative { get; }

        public virtual int ParametersCount { get; }

        public virtual int InitialPointsCount { get; }

        public virtual SvgCommandAction CommandAction { get; }

        public virtual SvgPoint GeneralPoint { get; }

        public abstract char ExportCommandName { get; }
    }
}

