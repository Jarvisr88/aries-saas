namespace DevExpress.XtraPrinting.Shape.Native
{
    using DevExpress.XtraReports.Native;
    using System;
    using System.ComponentModel;

    internal class RotatedShapePropertyDescriptor : PropertyDescriptorWrapper
    {
        public RotatedShapePropertyDescriptor(PropertyDescriptor oldPropertyDescriptor) : base(oldPropertyDescriptor)
        {
        }

        public override bool CanResetValue(object component) => 
            base.CanResetValue(this.GetActualComponent(component));

        private object GetActualComponent(object component)
        {
            RotatedShape shape = component as RotatedShape;
            return ((shape != null) ? shape.Shape : component);
        }

        public override object GetValue(object component) => 
            base.GetValue(this.GetActualComponent(component));

        public override void ResetValue(object component)
        {
            base.ResetValue(this.GetActualComponent(component));
        }

        public override void SetValue(object component, object value)
        {
            base.SetValue(this.GetActualComponent(component), value);
        }

        public override bool ShouldSerializeValue(object component) => 
            base.ShouldSerializeValue(this.GetActualComponent(component));
    }
}

