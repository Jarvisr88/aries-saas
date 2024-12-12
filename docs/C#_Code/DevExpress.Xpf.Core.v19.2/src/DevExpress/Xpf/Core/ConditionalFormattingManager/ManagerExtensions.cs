namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ManagerExtensions
    {
        public static ManagerItemViewModel Init(this ManagerItemViewModel item, ManagerViewModel vm)
        {
            item.SetOwner(vm);
            return item;
        }
    }
}

