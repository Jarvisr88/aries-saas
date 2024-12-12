namespace Devart.Common
{
    using System;

    public class DisplayNameAttribute : Attribute
    {
        private string a;

        public DisplayNameAttribute() : this(string.Empty)
        {
        }

        public DisplayNameAttribute(string A_0)
        {
            this.a = A_0;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            DisplayNameAttribute attribute = obj as DisplayNameAttribute;
            return ((attribute != null) && (attribute.DisplayName == this.DisplayName));
        }

        public override int GetHashCode() => 
            this.DisplayName.GetHashCode();

        public virtual string DisplayName =>
            this.a;
    }
}

