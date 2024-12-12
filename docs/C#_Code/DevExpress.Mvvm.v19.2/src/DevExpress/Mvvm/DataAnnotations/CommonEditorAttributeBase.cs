namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
    public abstract class CommonEditorAttributeBase : Attribute
    {
        protected CommonEditorAttributeBase()
        {
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || (base.GetType() != obj.GetType()))
            {
                return false;
            }
            CommonEditorAttributeBase base2 = (CommonEditorAttributeBase) obj;
            return ((base2 != null) && Equals(this.TemplateKey, base2.TemplateKey));
        }

        public override int GetHashCode() => 
            (((-1829533528 * -1521134295) + base.GetHashCode()) * -1521134295) + EqualityComparer<object>.Default.GetHashCode(this.TemplateKey);

        public object TemplateKey { get; set; }
    }
}

