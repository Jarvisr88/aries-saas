namespace ActiproSoftware.WinUICore
{
    using System;
    using System.ComponentModel;

    [AttributeUsage(AttributeTargets.Property, Inherited=true), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class ImageIndexAttribute : Attribute
    {
        private object #Uj;

        public ImageIndexAttribute() : this(null)
        {
        }

        public ImageIndexAttribute(object context)
        {
            this.#Uj = context;
        }

        public object Context
        {
            get => 
                this.#Uj;
            set => 
                this.#Uj = value;
        }
    }
}

