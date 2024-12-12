namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;

    public interface IValidationService
    {
        bool Execute(Func<bool> action);
    }
}

