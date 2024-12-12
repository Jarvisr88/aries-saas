namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using System;

    public interface IConditionEditor
    {
        bool CanInit(BaseEditUnit unit);
        BaseEditUnit Edit();
        void Init(BaseEditUnit unit);
        bool Validate();

        string Description { get; }
    }
}

