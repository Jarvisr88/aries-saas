namespace DevExpress.Xpf.Editors
{
    using System;

    public class ValueEditingAction : ValueEditingActionBase
    {
        public ValueEditingAction(Func<string, string> updateTextAction) : base(updateTextAction)
        {
        }

        public override string Process(string text, ValueEditingData data)
        {
            throw new NotImplementedException();
        }
    }
}

