namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;

    public interface IBindingCalculator
    {
        void Init(ITypeResolver typeResolver);
        object Resolve(object[] values);

        IEnumerable<Operand> Operands { get; }
    }
}

