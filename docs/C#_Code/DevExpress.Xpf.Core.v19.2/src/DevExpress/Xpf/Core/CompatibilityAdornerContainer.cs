namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class CompatibilityAdornerContainer : Grid
    {
        public void Destroy()
        {
            if (this.Adorner != null)
            {
                this.Adorner.Destroy();
                base.Children.Remove(this.Adorner);
            }
        }

        public void Initialize(CompatibilityAdorner adorner)
        {
            this.Adorner = adorner;
            if (!base.Children.Contains(this.Adorner))
            {
                base.Children.Add(this.Adorner);
            }
        }

        public CompatibilityAdorner Adorner { get; private set; }
    }
}

