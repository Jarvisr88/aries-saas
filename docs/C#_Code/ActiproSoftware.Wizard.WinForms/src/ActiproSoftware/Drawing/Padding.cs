namespace ActiproSoftware.Drawing
{
    using #Fqe;
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(#Jqe))]
    public struct Padding
    {
        private int #4n;
        private int #1n;
        private int #3n;
        private int #2n;
        public Padding(int allSides)
        {
            this.#1n = allSides;
            this.#2n = allSides;
            this.#3n = allSides;
            this.#4n = allSides;
        }

        public Padding(int left, int top, int right, int bottom)
        {
            this.#1n = left;
            this.#2n = top;
            this.#3n = right;
            this.#4n = bottom;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllSidesEqual =>
            (this.#1n == this.#2n) && ((this.#2n == this.#3n) && (this.#3n == this.#4n));
        [Category("Behavior"), Description("All values."), DefaultValue(0), RefreshProperties(RefreshProperties.All)]
        public int All
        {
            get => 
                this.AllSidesEqual ? this.#1n : 0;
            set
            {
                this.#1n = value;
                this.#2n = value;
                this.#3n = value;
                this.#4n = value;
            }
        }
        [Category("Behavior"), Description("The bottom value."), DefaultValue(0), RefreshProperties(RefreshProperties.All)]
        public int Bottom
        {
            get => 
                this.#4n;
            set => 
                this.#4n = value;
        }
        public static Padding Empty =>
            new Padding(-1);
        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Padding))
            {
                return false;
            }
            Padding padding = this;
            Padding padding2 = (Padding) obj;
            return ((padding.Left == padding2.Left) && ((padding.Top == padding2.Top) && ((padding.Right == padding2.Right) && (padding.Bottom == padding2.Bottom))));
        }

        public override int GetHashCode() => 
            this.#1n.GetHashCode();

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Horizontal =>
            Math.Max(0, this.#1n + this.#3n);
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEmpty =>
            (this.#4n == -1) && ((this.#1n == -1) && ((this.#3n == -1) && (this.#2n == -1)));
        [Category("Behavior"), Description("The left value."), DefaultValue(0), RefreshProperties(RefreshProperties.All)]
        public int Left
        {
            get => 
                this.#1n;
            set => 
                this.#1n = value;
        }
        [Category("Behavior"), Description("The right value."), DefaultValue(0), RefreshProperties(RefreshProperties.All)]
        public int Right
        {
            get => 
                this.#3n;
            set => 
                this.#3n = value;
        }
        [Category("Behavior"), Description("The top value."), DefaultValue(0), RefreshProperties(RefreshProperties.All)]
        public int Top
        {
            get => 
                this.#2n;
            set => 
                this.#2n = value;
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Vertical =>
            Math.Max(0, this.#2n + this.#4n);
        public static Padding Zero =>
            new Padding(0);
        public static bool operator ==(Padding left, Padding right) => 
            left.Equals(right);

        public static bool operator !=(Padding left, Padding right) => 
            !(left == right);
    }
}

