namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    public class GlobalBarItemScopeKey
    {
        public GlobalBarItemScopeKey(string name);
        public GlobalBarItemScopeKey(int scopeId, string name);
        public GlobalBarItemScopeKey(string scopeName, string name);
        public override bool Equals(object obj);
        public override int GetHashCode();
        public override string ToString();

        public string ScopeName { get; protected set; }

        public int ScopeId { get; protected set; }

        public string Name { get; protected set; }
    }
}

