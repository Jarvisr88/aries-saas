namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Windows;

    public class FormatInfoCollection : FreezableCollection<FormatInfo>
    {
        protected override Freezable CreateInstanceCore() => 
            new FormatInfoCollection();

        public FormatInfo this[string name] =>
            this.FirstOrDefault<FormatInfo>(x => x.FormatName == name);
    }
}

