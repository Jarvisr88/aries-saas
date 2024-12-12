namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class AnonymousEqualityComparer<T> : EqualityComparer<T>
    {
        private Func<T, T, bool> _equals;
        private Func<T, int> _getHashCode;

        public AnonymousEqualityComparer(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            Guard.ArgumentNotNull(equals, "equals");
            Guard.ArgumentNotNull(getHashCode, "getHashCode");
            this._equals = equals;
            this._getHashCode = getHashCode;
        }

        public override bool Equals(T x, T y) => 
            this._equals(x, y);

        public override int GetHashCode(T obj) => 
            this._getHashCode(obj);
    }
}

