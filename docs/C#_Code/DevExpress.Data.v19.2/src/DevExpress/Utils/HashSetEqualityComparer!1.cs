namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;

    public class HashSetEqualityComparer<T> : EqualityComparer<HashSet<T>>
    {
        public override bool Equals(HashSet<T> x, HashSet<T> y) => 
            (x != null) ? ((y != null) ? x.SetEquals(y) : false) : ReferenceEquals(y, null);

        public override int GetHashCode(HashSet<T> obj) => 
            HashCodeHelper.OrderInsensitive.CombineGenericList<T>(HashCodeHelper.Calculate(obj.Count), obj);
    }
}

