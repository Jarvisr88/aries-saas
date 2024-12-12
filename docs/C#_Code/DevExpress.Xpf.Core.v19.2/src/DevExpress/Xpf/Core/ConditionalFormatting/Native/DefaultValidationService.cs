namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;

    internal class DefaultValidationService : IValidationService
    {
        public bool Execute(Func<bool> action) => 
            action();
    }
}

