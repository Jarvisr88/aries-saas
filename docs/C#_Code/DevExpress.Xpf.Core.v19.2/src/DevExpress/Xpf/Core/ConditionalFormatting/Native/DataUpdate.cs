namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class DataUpdate
    {
        private readonly IFormatInfoProvider oldProvider;
        private readonly IFormatInfoProvider newProvider;

        public DataUpdate(IFormatInfoProvider oldProvider, IFormatInfoProvider newProvider)
        {
            if (oldProvider == null)
            {
                throw new ArgumentNullException("getOldValue");
            }
            if (newProvider == null)
            {
                throw new ArgumentNullException("getNewValue");
            }
            this.oldProvider = oldProvider;
            this.newProvider = newProvider;
        }

        public FormatValueProvider GetNewValue(string fieldName) => 
            this.newProvider.GetValueProvider(fieldName);

        public FormatValueProvider GetOldValue(string fieldName) => 
            this.oldProvider.GetValueProvider(fieldName);

        public IValidationService ValidationService { get; set; }
    }
}

