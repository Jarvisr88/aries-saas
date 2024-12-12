namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Media;

    public class HSBColor : ColorBase
    {
        private int h;
        private int s;
        private int b;

        public HSBColor(Color color)
        {
            base.ColorMode = ColorPickerColorMode.HSB;
            base.Color = color;
        }

        public HSBColor(int h, int s, int b) : this(h, s, b, 0xff)
        {
        }

        public HSBColor(int h, int s, int b, int a)
        {
            base.ColorMode = ColorPickerColorMode.HSB;
            this.h = h;
            this.s = s;
            this.b = b;
            base.A = a;
            base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor));
        }

        protected override void UpdateColor()
        {
            Color color = new Color();
            double b = 0.0;
            double b = 0.0;
            double b = 0.0;
            int num4 = Convert.ToInt32(Math.Floor((double) (((double) this.H) / 60.0)));
            if (num4 > 5)
            {
                num4 = 0;
            }
            double num5 = ((100.0 - this.S) * this.B) / 100.0;
            double num6 = ((this.B - num5) * (((double) this.H) % 60.0)) / 60.0;
            double num7 = num5 + num6;
            double num8 = this.B - num6;
            switch (num4)
            {
                case 0:
                    b = this.B;
                    b = num7;
                    b = num5;
                    break;

                case 1:
                    b = num8;
                    b = this.B;
                    b = num5;
                    break;

                case 2:
                    b = num5;
                    b = this.B;
                    b = num7;
                    break;

                case 3:
                    b = num5;
                    b = num8;
                    b = this.B;
                    break;

                case 4:
                    b = num7;
                    b = num5;
                    b = this.B;
                    break;

                case 5:
                    b = this.B;
                    b = num5;
                    b = num8;
                    break;

                default:
                    break;
            }
            color.R = Convert.ToByte((double) (b * 2.55));
            color.G = Convert.ToByte((double) (b * 2.55));
            color.B = Convert.ToByte((double) (b * 2.55));
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
            this.S = num4.IsZero() ? 0 : Convert.ToInt32((double) ((1.0 - (num5 / num4)) * 100.0));
            this.B = Convert.ToByte((double) (num4 * 100.0));
            base.A = base.Color.A;
        }

        public int H
        {
            get => 
                this.h;
            set => 
                base.SetProperty<int>(ref this.h, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(HSBColor)), (MethodInfo) methodof(HSBColor.get_H)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }

        public int S
        {
            get => 
                this.s;
            set => 
                base.SetProperty<int>(ref this.s, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(HSBColor)), (MethodInfo) methodof(HSBColor.get_S)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }

        public int B
        {
            get => 
                this.b;
            set => 
                base.SetProperty<int>(ref this.b, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(HSBColor)), (MethodInfo) methodof(HSBColor.get_B)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }
    }
}

