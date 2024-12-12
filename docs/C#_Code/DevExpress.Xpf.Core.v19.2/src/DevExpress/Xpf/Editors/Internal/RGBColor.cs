namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Media;

    public class RGBColor : ColorBase
    {
        private int r;
        private int g;
        private int b;

        public RGBColor(Color color)
        {
            base.ColorMode = ColorPickerColorMode.RGB;
            base.Color = color;
        }

        public RGBColor(int r, int g, int b) : this(r, g, b, 0xff)
        {
        }

        public RGBColor(int r, int g, int b, int a)
        {
            base.ColorMode = ColorPickerColorMode.RGB;
            this.r = r;
            this.g = g;
            this.b = b;
            base.A = a;
            base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor));
        }

        protected override void UpdateColor()
        {
            Color color = new Color {
                R = Convert.ToByte(this.R),
                G = Convert.ToByte(this.G),
                B = Convert.ToByte(this.B),
                A = Convert.ToByte(base.A)
            };
            base.Color = color;
        }

        protected override void UpdateValue()
        {
            this.R = base.Color.R;
            this.G = base.Color.G;
            this.B = base.Color.B;
            base.A = base.Color.A;
        }

        public int R
        {
            get => 
                this.r;
            set => 
                base.SetProperty<int>(ref this.r, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(RGBColor)), (MethodInfo) methodof(RGBColor.get_R)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }

        public int G
        {
            get => 
                this.g;
            set => 
                base.SetProperty<int>(ref this.g, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(RGBColor)), (MethodInfo) methodof(RGBColor.get_G)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }

        public int B
        {
            get => 
                this.b;
            set => 
                base.SetProperty<int>(ref this.b, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(RGBColor)), (MethodInfo) methodof(RGBColor.get_B)), new ParameterExpression[0]), () => base.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor)));
        }
    }
}

