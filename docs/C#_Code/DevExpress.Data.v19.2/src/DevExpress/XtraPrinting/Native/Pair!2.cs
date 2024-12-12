namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Text;

    public class Pair<TFirst, TSecond>
    {
        private TFirst first;
        private TSecond second;

        public Pair()
        {
        }

        public Pair(TFirst first, TSecond second)
        {
            this.first = first;
            this.second = second;
        }

        public Pair<TFirst, TSecond> Clone() => 
            new Pair<TFirst, TSecond>(this.first, this.second);

        public override bool Equals(object obj)
        {
            Pair<TFirst, TSecond> pair = obj as Pair<TFirst, TSecond>;
            return ((pair != null) ? (Equals(this.first, pair.First) && Equals(this.second, pair.Second)) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (this.First != null)
            {
                builder.Append(this.First.ToString());
            }
            builder.Append(", ");
            if (this.Second != null)
            {
                builder.Append(this.Second.ToString());
            }
            builder.Append(']');
            return builder.ToString();
        }

        public TFirst First
        {
            get => 
                this.first;
            set => 
                this.first = value;
        }

        public TSecond Second
        {
            get => 
                this.second;
            set => 
                this.second = value;
        }
    }
}

