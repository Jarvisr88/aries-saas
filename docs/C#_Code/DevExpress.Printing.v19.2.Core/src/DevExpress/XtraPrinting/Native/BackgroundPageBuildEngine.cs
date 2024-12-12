namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class BackgroundPageBuildEngine : IterativePageBuildEngine
    {
        protected BackgroundPageBuildEngineStrategy svc;

        public BackgroundPageBuildEngine(PrintingDocument document, BackgroundPageBuildEngineStrategy svc);
        protected override void Build();
        protected override void BuildPage();
        public override int GetBufferSize(int count);
        private void OnTick(object sender, EventArgs e);
        protected override void StopIteration();
    }
}

