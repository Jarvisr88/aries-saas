namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class TypeInfo
    {
        public TypeInfo(System.Type type) : this(type, null)
        {
        }

        public TypeInfo(System.Type type, string name)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            this.Type = type;
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            TypeInfo info = obj as TypeInfo;
            return ((info != null) ? ((this.Name == info.Name) && (this.Type == info.Type)) : false);
        }

        public override int GetHashCode()
        {
            string name = this.Name;
            string text2 = name;
            if (name == null)
            {
                string local1 = name;
                text2 = string.Empty;
            }
            return (this.Type.GetHashCode() ^ text2.GetHashCode());
        }

        public override string ToString() => 
            this.Name ?? this.Type.Name;

        public System.Type Type { get; private set; }

        public string Name { get; private set; }
    }
}

