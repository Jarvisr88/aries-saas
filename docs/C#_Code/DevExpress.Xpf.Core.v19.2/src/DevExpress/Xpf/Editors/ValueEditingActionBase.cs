namespace DevExpress.Xpf.Editors
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class ValueEditingActionBase
    {
        public ValueEditingActionBase(Func<string, string> updateTextAction)
        {
            Guard.ArgumentNotNull(updateTextAction, "updateTextAction");
            this.UpdateTextAction = updateTextAction;
        }

        public abstract string Process(string text, ValueEditingData data);

        private Func<string, string> UpdateTextAction { get; set; }
    }
}

