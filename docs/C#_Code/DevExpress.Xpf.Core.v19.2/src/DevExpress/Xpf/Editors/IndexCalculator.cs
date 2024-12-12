namespace DevExpress.Xpf.Editors
{
    using System;

    public class IndexCalculator
    {
        private const double Epsilon = 1E-09;
        protected const double MagicNumber = 100.0;

        public bool AreClose(double value1, double value2) => 
            Math.Abs((double) (value1 - value2)) < 1E-09;

        public bool AreLessOrClose(double value1, double value2) => 
            this.AreClose(value1, value2) || (value1 < value2);

        public double CalcIndexOffset(double offset, double logicalViewport, double viewport, double itemOffset, double tapPointOffset)
        {
            double num = logicalViewport / 2.0;
            double num2 = logicalViewport / viewport;
            itemOffset = (itemOffset * num2) - 50.0;
            return this.Floor(((offset + (tapPointOffset * num2)) - num) - itemOffset);
        }

        public double CalcRelativePosition(double offset)
        {
            double num = this.LogicalToNormalizedOffset(offset);
            return (this.Floor(num) - num);
        }

        public virtual double CalcStart(double extent) => 
            0.0;

        public double Floor(double value)
        {
            double num = Math.Round(value);
            return (!this.AreClose(num, value) ? Math.Floor(value) : num);
        }

        public int GetIndex(int index, int count, bool isLooped)
        {
            if (count == 0)
            {
                return 0;
            }
            if (!isLooped)
            {
                return index;
            }
            int num2 = Math.Abs(index) % count;
            return ((Math.Sign(index) >= 0) ? num2 : (count - num2));
        }

        public double IndexToLogicalOffset(double index) => 
            index * 100.0;

        public double IndexToLogicalOffset(int index) => 
            this.IndexToLogicalOffset((double) index);

        public int LogicalOffsetToIndex(double offset, int count, bool isLooped)
        {
            int index = Convert.ToInt32(this.Floor(offset / 100.0));
            return this.GetIndex(index, count, isLooped);
        }

        public double LogicalToNormalizedOffset(double offset) => 
            offset / 100.0;

        public double OffsetToLogicalOffset(double offset, double viewport)
        {
            double num = this.CalcStart(viewport);
            return (offset - num);
        }
    }
}

