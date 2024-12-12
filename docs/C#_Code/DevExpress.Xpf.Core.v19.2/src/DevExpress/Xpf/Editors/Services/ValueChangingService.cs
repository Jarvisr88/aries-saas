namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;

    public class ValueChangingService : BaseEditBaseService
    {
        public ValueChangingService(BaseEdit editor) : base(editor)
        {
        }

        public virtual void SetIsValueChanged(bool value)
        {
            this.IsValueChanged = value;
        }

        public bool IsValueChanged { get; private set; }
    }
}

