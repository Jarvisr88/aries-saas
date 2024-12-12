namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    public class ElementRegistratorChangedArgs : EventArgs
    {
        public ElementRegistratorChangedArgs(IBarNameScopeSupport element, object name, object oldName, ElementRegistratorChangeType changeType);

        public IBarNameScopeSupport Element { get; private set; }

        public object Name { get; private set; }

        public object OldName { get; private set; }

        public ElementRegistratorChangeType ChangeType { get; private set; }
    }
}

