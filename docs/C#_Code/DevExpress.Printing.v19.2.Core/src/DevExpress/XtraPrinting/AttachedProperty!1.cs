namespace DevExpress.XtraPrinting
{
    using System;

    public sealed class AttachedProperty<T> : AttachedPropertyBase
    {
        internal AttachedProperty(string name) : base(name)
        {
        }

        public override Type PropertyType =>
            typeof(T);
    }
}

