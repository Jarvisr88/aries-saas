namespace DevExpress.Data.Access
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;

    public abstract class ComplexPropertyDescriptor : PropertyDescriptor
    {
        protected DataControllerBase controller;
        protected string path;
        private bool isReady;
        protected string[] split;
        protected object sourceObject;

        public ComplexPropertyDescriptor(DataControllerBase controller, string path);
        public ComplexPropertyDescriptor(object sourceObject, string path);
        public override bool CanResetValue(object component);
        public abstract object GetOwnerOfLast(object component);
        protected virtual void Prepare();
        protected void PrepareSplit();
        public override void ResetValue(object component);
        public override bool ShouldSerializeValue(object component);

        protected bool IsReady { get; set; }

        public override string Category { get; }
    }
}

