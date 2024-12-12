namespace ActiproSoftware.Drawing
{
    using #H;
    using System;

    public class Range
    {
        private int #i3c;
        private int #Jec;

        public Range()
        {
        }

        public Range(int min, int max)
        {
            this.#i3c = min;
            this.#Jec = max;
        }

        public bool Contains(Range range) => 
            (range.Min >= this.#i3c) && (range.Max <= this.#Jec);

        public bool Contains(int value) => 
            (value >= this.#i3c) && (value < this.#Jec);

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Range))
            {
                return false;
            }
            Range range = this;
            Range range2 = (Range) obj;
            return ((range.#i3c == range2.#i3c) && (range.#Jec == range2.#Jec));
        }

        public override int GetHashCode() => 
            this.#i3c.GetHashCode() + this.#Jec.GetHashCode();

        public bool IntersectsWith(Range range) => 
            (range.Max >= this.#i3c) && (range.Min <= this.#Jec);

        public void Normalize()
        {
            if (!this.IsNormalized)
            {
                int num = this.#i3c;
                this.#i3c = this.#Jec;
                this.#Jec = num;
            }
        }

        public static bool operator ==(Range left, Range right) => 
            (left != null) ? left.Equals(right) : (right == null);

        public static bool operator !=(Range left, Range right) => 
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

        public void Union(Range range)
        {
            int num = Math.Min(Math.Min(this.#i3c, this.#Jec), Math.Min(range.Min, range.Max));
            int num2 = Math.Max(Math.Max(this.#i3c, this.#Jec), Math.Max(range.Min, range.Max));
            this.#i3c = num;
            this.#Jec = num2;
        }

        public static Range Empty =>
            new Range(-1, -1);

        public bool IsEmpty =>
            (this.#i3c == -1) && (this.#Jec == -1);

        public bool IsNormalized =>
            this.#Jec >= this.#i3c;

        public int Length =>
            this.#Jec - this.#i3c;

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

