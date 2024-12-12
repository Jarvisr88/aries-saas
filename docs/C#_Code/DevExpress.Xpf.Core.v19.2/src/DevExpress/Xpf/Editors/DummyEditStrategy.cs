namespace DevExpress.Xpf.Editors
{
    using System;

    public class DummyEditStrategy : EditStrategyBase
    {
        public DummyEditStrategy(BaseEdit editor) : base(editor)
        {
        }

        protected override object GetValueForDisplayText()
        {
            throw new NotImplementedException();
        }

        protected override void SyncWithEditorInternal()
        {
            throw new NotImplementedException();
        }

        protected override void SyncWithValueInternal()
        {
            throw new NotImplementedException();
        }
    }
}

