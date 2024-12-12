namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Media;

    public class HLSColor : ColorBase
    {
        private int h;
        private int l;
        private int s;

        public HLSColor(Color color)
        {
            base.ColorMode = ColorPickerColorMode.HLS;
            base.Color = color;
        }

        public HLSColor(int h, int l, int s) : this(h, l, s, 0xff)
        {
        }

        public HLSColor(int h, int l, int s, int a)
        {
            base.ColorMode = ColorPickerColorMode.HLS;
            this.h = h;
            this.l = l;
            this.s = s;
            base.A = a;
            base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor));
        }

        private double HLSColorCalc(double c, double p, double q)
        {
            if (c.LessThan(0.0))
            {
                c++;
            }
            if (c.GreaterThan(1.0))
            {
                c--;
            }
            return (!c.LessThan(0.16666666666666666) ? ((!c.LessThan(0.5) || !c.GreaterThan(0.16666666666666666)) ? ((!c.GreaterThan(0.5) || !c.LessThan(0.66666666666666663)) ? p : (p + (((q - p) * (0.66666666666666663 - c)) * 6.0))) : q) : (p + (((q - p) * 6.0) * c)));
        }

        protected override void UpdateColor()
        {
            Color color = new Color();
            double num = ((double) this.S) / 100.0;
            double num2 = ((double) this.L) / 100.0;
            double q = num2.LessThan(0.5) ? (num2 * (1.0 + num)) : ((num2 + num) - (num2 * num));
            double p = (2.0 * num2) - q;
            double c = ((double) this.H) / 360.0;
            color.R = Convert.ToByte((double) (this.HLSColorCalc(c + 0.33333333333333331, p, q) * 255.0));
            color.G = Convert.ToByte((double) (this.HLSColorCalc(c, p, q) * 255.0));
            color.B = Convert.ToByte((double) (this.HLSColorCalc(c - 0.33333333333333331, p, q) * 255.0));
            color.A = Convert.ToByte(base.A);
            base.Color = color;
        }

        protected override void UpdateValue()
        {
            double num = ((double) base.Color.R) / 255.0;
            double num2 = ((double) base.Color.G) / 255.0;
            double num3 = ((double) base.Color.B) / 255.0;
            double num4 = Math.Max(num, Math.Max(num2, num3));
            double num5 = Math.Min(num, Math.Min(num2, num3));
            if (num4.AreClose(num5))
            {
                this.H = 0;
            }
            else if (num4.AreClose(num) && num2.GreaterThanOrClose(num3))
            {
                this.H = Convert.ToInt32((double) ((60.0 * (num2 - num3)) / (num4 - num5)));
            }
            else if (num4.AreClose(num) && num2.LessThan(num3))
            {
                this.H = Convert.ToInt32((double) (((60.0 * (num2 - num3)) / (num4 - num5)) + 360.0));
            }
            else if (num4.AreClose(num2))
            {
                this.H = Convert.ToInt32((double) (((60.0 * (num3 - num)) / (num4 - num5)) + 120.0));
            }
            else if (num4.AreClose(num3))
            {
                this.H = Convert.ToInt32((double) (((60.0 * (num - num2)) / (num4 - num5)) + 240.0));
            }
            this.L = Convert.ToByte((double) ((num4 + num5) * 50.0));
            this.S = num4.AreClose(num5) ? 0 : Convert.ToByte((double) (((num4 - num5) / (1.0 - Math.Abs((double) (1.0 - (num4 + num5))))) * 100.0));
            base.A = base.Color.A;
        }

        public int H
        {
            get => 
                this.h;
            set => 
                base.SetProperty<int>(ref this.h, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(HLSColor)), (MethodInfo) methodof(HLSColor.get_H)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }

        public int L
        {
            get => 
                this.l;
            set => 
                base.SetProperty<int>(ref this.l, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(HLSColor)), (MethodInfo) methodof(HLSColor.get_L)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }

        public int S
        {
            get => 
                this.s;
            set => 
                base.SetProperty<int>(ref this.s, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(HLSColor)), (MethodInfo) methodof(HLSColor.get_S)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }
    }
}

