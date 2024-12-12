namespace DevExpress.Utils
{
    using System;
    using System.Linq;

    public class MultiKey
    {
        protected object[] keyParts;
        private int hashCode;

        public MultiKey(params object[] keyParts)
        {
            if (keyParts == null)
            {
                throw new ArgumentNullException("keyParts");
            }
            this.keyParts = keyParts;
            this.hashCode = HashCodeHelper.CalculateGenericList<object>(keyParts);
        }

        public static MultiKey CreateKey(MultiKey source, params object[] keyParts) => 
            new MultiKey(source.keyParts.Concat<object>(keyParts).ToArray<object>());

        public override bool Equals(object obj)
        {
            MultiKey key = obj as MultiKey;
            if ((key == null) || ((base.GetType() != key.GetType()) || (this.keyParts.Length != key.keyParts.Length)))
            {
                return false;
            }
            int length = this.keyParts.Length;
            for (int i = 0; i < length; i++)
            {
                if (!Equals(this.keyParts[i], key.keyParts[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode() => 
            this.hashCode;
    }
}

