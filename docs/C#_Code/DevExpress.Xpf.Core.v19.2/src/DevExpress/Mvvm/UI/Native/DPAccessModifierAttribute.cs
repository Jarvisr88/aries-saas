namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DPAccessModifierAttribute : Attribute
    {
        public DPAccessModifierAttribute(MemberVisibility setterVisibility = 0, MemberVisibility getterVisibility = 0, bool nonBrowsable = false)
        {
            this.SetterVisibility = setterVisibility;
            this.GetterVisibility = getterVisibility;
            this.NonBrowsable = nonBrowsable;
        }

        public MemberVisibility SetterVisibility { get; set; }

        public MemberVisibility GetterVisibility { get; set; }

        public bool NonBrowsable { get; set; }
    }
}

