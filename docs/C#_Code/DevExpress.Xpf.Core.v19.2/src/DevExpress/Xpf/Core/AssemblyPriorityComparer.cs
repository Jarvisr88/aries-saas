namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class AssemblyPriorityComparer : IComparer<string>
    {
        public int Compare(string x, string y) => 
            Math.Sign((int) (this.GetLevel(y) - this.GetLevel(x)));

        private int GetLevel(string assemblyFullName) => 
            !AssemblyHelper.IsDXThemeAssembly(assemblyFullName) ? (AssemblyHelper.IsDXProductAssembly(assemblyFullName) ? 0 : (!AssemblyHelper.IsEntryAssembly(assemblyFullName) ? 2 : 3)) : 1;
    }
}

