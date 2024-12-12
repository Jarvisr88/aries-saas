namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;

    public interface ICommandCalculator
    {
        bool CanExecute(object[] opValues, object parameter);
        void Execute(object[] opValues, object parameter);
        void Init(ITypeResolver typeResolver);

        IEnumerable<Operand> Operands { get; }
    }
}

