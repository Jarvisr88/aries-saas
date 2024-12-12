namespace DevExpress.Data.Filtering
{
    using System;

    public interface ICustomFunctionDisplayAttributes : ICustomFunctionOperatorBrowsable, ICustomFunctionOperator
    {
        string DisplayName { get; }

        object Image { get; }
    }
}

