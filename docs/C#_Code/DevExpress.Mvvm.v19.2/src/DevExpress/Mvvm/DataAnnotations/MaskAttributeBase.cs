namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property)]
    public abstract class MaskAttributeBase : Attribute
    {
        protected MaskAttributeBase()
        {
            this.UseAsDisplayFormat = true;
        }

        internal virtual bool IsDefaultMask(string mask) => 
            string.IsNullOrEmpty(mask);

        public string Mask { get; set; }

        public bool UseAsDisplayFormat { get; set; }

        internal CultureInfo CultureInternal
        {
            get
            {
                CultureInfo cultureInfoCore = this.CultureInfoCore;
                CultureInfo info2 = cultureInfoCore;
                if (cultureInfoCore == null)
                {
                    CultureInfo local1 = cultureInfoCore;
                    if (!string.IsNullOrEmpty(this.CultureNameCore))
                    {
                        return new CultureInfo(this.CultureNameCore);
                    }
                    info2 = null;
                }
                return info2;
            }
        }

        protected virtual string CultureNameCore =>
            null;

        protected virtual CultureInfo CultureInfoCore =>
            null;

        internal virtual bool AutomaticallyAdvanceCaretInternal =>
            false;

        internal virtual bool IgnoreBlankInternal =>
            true;

        internal virtual char PlaceHolderInternal =>
            '_';

        internal virtual bool SaveLiteralInternal =>
            true;

        internal virtual bool ShowPlaceHoldersInternal =>
            true;
    }
}

