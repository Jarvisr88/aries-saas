namespace ActiproSoftware.WinUICore.Rendering
{
    using #H;
    using System;

    public class UnicodeRange
    {
        private int #i3c;
        private int #Jec;

        public UnicodeRange()
        {
        }

        public UnicodeRange(int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.#i3c = min;
            this.#Jec = max;
        }

        public bool Contains(int value) => 
            (value >= this.#i3c) && (value <= this.#Jec);

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is UnicodeRange))
            {
                return false;
            }
            UnicodeRange range = this;
            UnicodeRange range2 = (UnicodeRange) obj;
            return ((range.#i3c == range2.#i3c) && (range.#Jec == range2.#Jec));
        }

        public override int GetHashCode() => 
            this.#i3c.GetHashCode() + this.#Jec.GetHashCode();

        public static bool operator ==(UnicodeRange left, UnicodeRange right) => 
            (left != null) ? left.Equals(right) : (right == null);

        public static bool operator !=(UnicodeRange left, UnicodeRange right) => 
            !(left == right);

        public override string ToString()
        {
            string[] textArray1 = new string[5];
            string[] textArray2 = new string[5];
            textArray2[0] = #G.#eg(0xdfb);
            string[] local2 = textArray2;
            string[] local3 = textArray2;
            local3[1] = this.#i3c.ToString();
            string[] local1 = local3;
            local1[2] = #G.#eg(0xe04);
            local1[3] = this.#Jec.ToString();
            local1[4] = #G.#eg(0xb4a);
            return string.Concat(local1);
        }

        public int Max
        {
            get => 
                this.#Jec;
            set => 
                this.#Jec = value;
        }

        public int Min
        {
            get => 
                this.#i3c;
            set => 
                this.#i3c = value;
        }
    }
}

