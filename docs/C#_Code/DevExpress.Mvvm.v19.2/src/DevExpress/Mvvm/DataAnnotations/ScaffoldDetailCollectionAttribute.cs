namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class ScaffoldDetailCollectionAttribute : Attribute
    {
        public const bool DefaultScaffold = true;

        public ScaffoldDetailCollectionAttribute() : this(true)
        {
        }

        public ScaffoldDetailCollectionAttribute(bool scaffold)
        {
            this.Scaffold = scaffold;
        }

        public bool Scaffold { get; private set; }
    }
}

