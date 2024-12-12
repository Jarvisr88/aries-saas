namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class BooleanItemsSourceExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (!this.FirstValue)
            {
                bool[] flagArray1 = new bool[2];
                flagArray1[1] = true;
                return flagArray1;
            }
            bool[] flagArray2 = new bool[2];
            flagArray2[0] = true;
            return flagArray2;
        }

        [DefaultValue(true)]
        public bool FirstValue { get; set; }
    }
}

