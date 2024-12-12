namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;

    public interface IEventCalculator
    {
        void Event(object[] opValues, object sender, object args);
        void Init(ITypeResolver typeResolver);

        IEnumerable<Operand> Operands { get; }
    }
}

