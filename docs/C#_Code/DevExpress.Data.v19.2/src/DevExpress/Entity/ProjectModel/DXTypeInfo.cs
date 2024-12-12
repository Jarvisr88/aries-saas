namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class DXTypeInfo : IDXTypeInfo, IHasName
    {
        protected Type type;

        public DXTypeInfo(Type type)
        {
            this.type = type;
            this.Name = type.Name;
            if (type.IsNested)
            {
                Type declaringType = type.DeclaringType;
                StringBuilder builder = new StringBuilder(declaringType.Name);
                while (true)
                {
                    if (!declaringType.IsNested)
                    {
                        this.DeclaringTypeName = builder.ToString();
                        break;
                    }
                    builder.Insert(0, '+');
                    declaringType = declaringType.DeclaringType;
                    builder.Insert(0, declaringType.Name);
                }
            }
            this.NamespaceName = type.Namespace;
        }

        public DXTypeInfo(string name, string namespaceName) : this(name, null, namespaceName)
        {
        }

        public DXTypeInfo(string name, string declaringTypeName, string namespaceName)
        {
            this.Name = name;
            this.DeclaringTypeName = declaringTypeName;
            this.NamespaceName = namespaceName;
        }

        public static string GetFullName(string namespaceName, string name) => 
            GetFullName(namespaceName, null, name);

        public static string GetFullName(string namespaceName, string declaringTypeName, string name)
        {
            StringBuilder builder = new StringBuilder(namespaceName);
            if (builder.Length != 0)
            {
                builder.Append('.');
            }
            if (!string.IsNullOrEmpty(declaringTypeName))
            {
                builder.Append(declaringTypeName);
                builder.Append('+');
            }
            builder.Append(name);
            return builder.ToString();
        }

        public Type ResolveType() => 
            this.type;

        public IDXAssemblyInfo Assembly { get; set; }

        public string Name { get; private set; }

        public string DeclaringTypeName { get; private set; }

        public string NamespaceName { get; private set; }

        public virtual string FullName =>
            GetFullName(this.NamespaceName, this.DeclaringTypeName, this.Name);

        public bool IsSolutionType =>
            (this.Assembly != null) && this.Assembly.IsSolutionAssembly;
    }
}

