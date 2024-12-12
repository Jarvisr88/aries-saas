namespace DevExpress.Entity.Model.Metadata
{
    using System;

    public class RuntimeBase
    {
        private readonly object value;

        protected RuntimeBase(object value)
        {
            this.value = value;
        }

        public override bool Equals(object obj) => 
            this == (obj as RuntimeBase);

        public override int GetHashCode() => 
            this.value.GetHashCode();

        public static bool operator ==(RuntimeBase r1, RuntimeBase r2)
        {
            bool flag = ReferenceEquals(r1, null);
            bool flag2 = ReferenceEquals(r2, null);
            return (!(flag & flag2) ? (!(flag | flag2) ? Equals(r1.value, r2.value) : false) : true);
        }

        public static bool operator !=(RuntimeBase r1, RuntimeBase r2) => 
            !(r1 == r2);

        public override string ToString() => 
            this.value.ToString();

        public object Value =>
            this.value;
    }
}

