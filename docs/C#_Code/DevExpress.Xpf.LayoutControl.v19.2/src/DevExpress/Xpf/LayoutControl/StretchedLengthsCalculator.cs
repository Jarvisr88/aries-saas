namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class StretchedLengthsCalculator : List<DevExpress.Xpf.LayoutControl.ItemInfo>
    {
        public StretchedLengthsCalculator(double availableLength)
        {
            this.AvailableLength = availableLength;
        }

        public void Calculate()
        {
            if (base.Count != 0)
            {
                double availableLength = double.IsInfinity(this.AvailableLength) ? this.ItemsLength : this.AvailableLength;
                while (true)
                {
                    double itemsLength = this.ItemsLength;
                    double num3 = 0.0;
                    double num5 = 0.0;
                    double num6 = 0.0;
                    double num7 = 0.0;
                    int num8 = 0;
                    while (true)
                    {
                        if (num8 >= base.Count)
                        {
                            if ((num6 == 0.0) && (num7 == 0.0))
                            {
                                return;
                            }
                            this.NeedsMoreLength = !(num6 == 0.0);
                            this.ProcessMinOrMaxLengthItems(num6 > num7, ref availableLength);
                            break;
                        }
                        DevExpress.Xpf.LayoutControl.ItemInfo info = base[num8];
                        if (!info.IsProcessed)
                        {
                            double num4 = (itemsLength == 0.0) ? this.GetUniformStretchedOffset(num8 + 1) : Math.Round((double) ((availableLength * (num3 + info.Length)) / itemsLength));
                            num3 += info.Length;
                            info.Length = num4 - num5;
                            num5 += info.Length;
                            if (info.Length < info.MinLength)
                            {
                                num6 += info.MinLength - info.Length;
                            }
                            if (info.Length > info.MaxLength)
                            {
                                num7 += info.Length - info.MaxLength;
                            }
                        }
                        num8++;
                    }
                }
            }
        }

        public bool CanFitItemsUniformly() => 
            !(this.ItemsLength == this.AvailableLength);

        protected double GetUniformStretchedOffset(int index) => 
            (base.Count != 1) ? Math.Round((double) ((this.AvailableLength * index) / ((double) base.Count))) : (this.AvailableLength * index);

        protected void ProcessMinOrMaxLengthItems(bool processMinLengthItems, ref double availableLength)
        {
            foreach (DevExpress.Xpf.LayoutControl.ItemInfo info in this)
            {
                if (!info.IsProcessed && ((processMinLengthItems && (info.Length < info.MinLength)) || (!processMinLengthItems && (info.Length > info.MaxLength))))
                {
                    info.Length = !processMinLengthItems ? info.MaxLength : info.MinLength;
                    info.IsProcessed = true;
                    availableLength -= info.Length;
                }
            }
            availableLength = Math.Max(0.0, availableLength);
        }

        public double AvailableLength { get; private set; }

        public bool NeedsMoreLength { get; private set; }

        protected double ItemsLength
        {
            get
            {
                double num = 0.0;
                foreach (DevExpress.Xpf.LayoutControl.ItemInfo info in this)
                {
                    if (!info.IsProcessed)
                    {
                        num += info.Length;
                    }
                }
                return num;
            }
        }
    }
}

