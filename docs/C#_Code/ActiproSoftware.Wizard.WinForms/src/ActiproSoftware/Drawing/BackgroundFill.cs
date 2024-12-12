namespace ActiproSoftware.Drawing
{
    using ActiproSoftware.ComponentModel;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Editor("ActiproSoftware.Drawing.Design.BackgroundFillEditor, ActiproSoftware.Shared.WinForms.Design, Version=20.1.402.0, Culture=neutral, PublicKeyToken=c27e062d3c1a4763", typeof(UITypeEditor)), TypeConverter(typeof(GenericExpandableNullableObjectConverter))]
    public abstract class BackgroundFill
    {
        [Category("Action"), Description("Occurs after a property is changed.")]
        public event EventHandler PropertyChanged;

        public abstract BackgroundFill Clone();
        public void Draw(Graphics g, Rectangle bounds)
        {
            this.Draw(g, bounds, bounds, Sides.Top);
        }

        public void Draw(Graphics g, Rectangle bounds, Sides side)
        {
            this.Draw(g, bounds, bounds, side);
        }

        public void Draw(Graphics g, Rectangle bounds, Rectangle brushBounds)
        {
            this.Draw(g, bounds, brushBounds, Sides.Top);
        }

        public abstract void Draw(Graphics g, Rectangle bounds, Rectangle brushBounds, Sides side);
        public override bool Equals(object obj) => 
            (obj != null) && !(obj.GetType().GetType() != base.GetType());

        public virtual Brush GetBrush(Rectangle bounds, Sides side) => 
            null;

        public override int GetHashCode() => 
            this.GetHashCode();

        protected virtual void OnPropertyChanged(EventArgs e)
        {
            if (this.#Sj != null)
            {
                this.#Sj(this, e);
            }
        }
    }
}

