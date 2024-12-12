namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using System;
    using System.Drawing.Printing;

    public abstract class MarginsFBase : ICloneable
    {
        private float left;
        private float right;
        private float top;
        private float bottom;

        public MarginsFBase()
        {
        }

        public MarginsFBase(float left, float right, float top, float bottom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }

        public abstract object Clone();
        public override bool Equals(object obj)
        {
            MarginsFBase base2 = obj as MarginsFBase;
            return ((base2 != null) && ((this.left == base2.left) && ((this.right == base2.right) && ((this.top == base2.top) && (this.bottom == base2.bottom)))));
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<int, int, int, int>((int) this.left, (int) this.right, (int) this.top, (int) this.bottom);

        public Margins Round() => 
            new Margins(Convert.ToInt32(this.Left), Convert.ToInt32(this.Right), Convert.ToInt32(this.Top), Convert.ToInt32(this.Bottom));

        public float Left
        {
            get => 
                this.left;
            set => 
                this.left = value;
        }

        public float Right
        {
            get => 
                this.right;
            set => 
                this.right = value;
        }

        public float Top
        {
            get => 
                this.top;
            set => 
                this.top = value;
        }

        public float Bottom
        {
            get => 
                this.bottom;
            set => 
                this.bottom = value;
        }
    }
}

