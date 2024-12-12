namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections;

    public class CustomizationEnumerator : IEnumerator
    {
        private readonly IEnumerator CustomizationControls;

        public CustomizationEnumerator(DockLayoutManager manager)
        {
            this.CustomizationControls = manager.CustomizationController.GetChildren().GetEnumerator();
        }

        bool IEnumerator.MoveNext() => 
            this.CustomizationControls.MoveNext();

        void IEnumerator.Reset()
        {
            this.CustomizationControls.Reset();
        }

        object IEnumerator.Current =>
            this.CustomizationControls.Current;
    }
}

