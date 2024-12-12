namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Media;

    public class CMYKColor : ColorBase
    {
        private int c;
        private int m;
        private int y;
        private int k;

        public CMYKColor(Color color)
        {
            base.ColorMode = ColorPickerColorMode.CMYK;
            base.Color = color;
        }

        public CMYKColor(int c, int m, int y, int k) : this(c, m, y, k, 0xff)
        {
        }

        public CMYKColor(int c, int m, int y, int k, int a)
        {
            base.ColorMode = ColorPickerColorMode.CMYK;
            this.c = c;
            this.m = m;
            this.y = y;
            this.k = k;
            base.A = a;
            base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor));
        }

        protected override void UpdateColor()
        {
            Color color = new Color {
                R = Convert.ToByte((double) (((1.0 - (((double) this.C) / 100.0)) * (1.0 - (((double) this.K) / 100.0))) * 255.0)),
                G = Convert.ToByte((double) (((1.0 - (((double) this.M) / 100.0)) * (1.0 - (((double) this.K) / 100.0))) * 255.0)),
                B = Convert.ToByte((double) (((1.0 - (((double) this.Y) / 100.0)) * (1.0 - (((double) this.K) / 100.0))) * 255.0)),
                A = Convert.ToByte(base.A)
            };
            base.Color = color;
        }

        protected override void UpdateValue()
        {
            double num = ((double) base.Color.R) / 255.0;
            double num2 = ((double) base.Color.G) / 255.0;
            double num3 = ((double) base.Color.B) / 255.0;
            double num4 = 1.0 - Math.Max(num, Math.Max(num2, num3));
            double num5 = num4.AreClose(1.0) ? 0.0 : (((1.0 - num) - num4) / (1.0 - num4));
            double num6 = num4.AreClose(1.0) ? 0.0 : (((1.0 - num2) - num4) / (1.0 - num4));
            double num7 = num4.AreClose(1.0) ? 0.0 : (((1.0 - num3) - num4) / (1.0 - num4));
            this.K = Convert.ToInt32((double) (100.0 * num4));
            this.C = Convert.ToInt32((double) (100.0 * num5));
            this.M = Convert.ToInt32((double) (100.0 * num6));
            this.Y = Convert.ToInt32((double) (100.0 * num7));
            base.A = base.Color.A;
        }

        public int C
        {
            get => 
                this.c;
            set => 
                base.SetProperty<int>(ref this.c, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(CMYKColor)), (MethodInfo) methodof(CMYKColor.get_C)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }

        public int M
        {
            get => 
                this.m;
            set => 
                base.SetProperty<int>(ref this.m, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(CMYKColor)), (MethodInfo) methodof(CMYKColor.get_M)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }

        public int Y
        {
            get => 
                this.y;
            set => 
                base.SetProperty<int>(ref this.y, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(CMYKColor)), (MethodInfo) methodof(CMYKColor.get_Y)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }

        public int K
        {
            get => 
                this.k;
            set => 
                base.SetProperty<int>(ref this.k, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(CMYKColor)), (MethodInfo) methodof(CMYKColor.get_K)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }
    }
}

