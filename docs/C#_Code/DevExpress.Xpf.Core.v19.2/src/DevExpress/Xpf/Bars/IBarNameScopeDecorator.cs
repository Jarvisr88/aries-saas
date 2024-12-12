namespace DevExpress.Xpf.Bars
{
    using System;

    public interface IBarNameScopeDecorator
    {
        void Attach(BarNameScope scope);
        void Detach();
    }
}

