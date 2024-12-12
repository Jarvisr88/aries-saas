namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using System;
    using System.Windows.Forms;

    public class ApplicationBackgroundPageBuildEngineStrategy : BackgroundPageBuildEngineStrategy
    {
        private Control control;

        public override event EventHandler Tick;

        public ApplicationBackgroundPageBuildEngineStrategy();
        public override void BeginInvoke(Action0 method);
    }
}

