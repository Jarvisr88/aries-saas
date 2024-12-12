namespace ActiproSoftware.Drawing
{
    using ActiproSoftware.ComponentModel;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Editor("ActiproSoftware.Drawing.Design.BorderEditor, ActiproSoftware.Shared.WinForms.Design, Version=20.1.402.0, Culture=neutral, PublicKeyToken=c27e062d3c1a4763", typeof(UITypeEditor)), TypeConverter(typeof(GenericExpandableNullableObjectConverter))]
    public abstract class Border
    {
        [Category("Action"), Description("Occurs after a property is changed.")]
        public event EventHandler PropertyChanged;

        public abstract Border Clone();
        public virtual void Draw(Graphics g, Rectangle bounds)
        {
            this.Draw(g, bounds, Sides.Left | Sides.Bottom | Sides.Right | Sides.Top);
        }

        public abstract void Draw(Graphics g, Rectangle bounds, Sides sides);
        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Border))
            {
                return false;
            }
            Border border1 = (Border) obj;
            return true;
        }

        public abstract int GetBorderWidth();
        public override int GetHashCode() => 
            this.GetHashCode();

        public abstract Color GetPrimaryColor();
        protected virtual void OnPropertyChanged(EventArgs e)
        {
            if (this.#Sj != null)
            {
                this.#Sj(this, e);
            }
        }
    }
}

